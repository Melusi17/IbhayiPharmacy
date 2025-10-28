using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Pharmacist")]
    public class WalkInPrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailService _email; // ADDED: Email service
        private readonly Random _random = new Random();

        // UPDATED: Constructor with EmailService
        public WalkInPrescriptionController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, EmailService email)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _email = email; // ADDED
        }

        // GET: Main walk-in prescription creation form
        public async Task<IActionResult> Create()
        {
            try
            {
                var viewModel = new WalkInPrescriptionVM
                {
                    PrescriptionDate = DateTime.Now,
                    ScriptLines = new List<WalkInScriptLineVM>
                    {
                        new WalkInScriptLineVM
                        {
                            Status = "Approved" // Default to Approved for walk-ins
                        }
                    }
                };

                await LoadViewData();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading form: {ex.Message}";
                return View(new WalkInPrescriptionVM());
            }
        }

        // POST: Save walk-in prescription and create order (UPDATED WITH EMAIL FUNCTIONALITY)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WalkInPrescriptionVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await LoadViewData();
                    return View(model);
                }

                // Validate required data
                if (!model.CustomerId.HasValue)
                {
                    ModelState.AddModelError("", "Please select or register a patient.");
                    await LoadViewData();
                    return View(model);
                }

                if (!model.DoctorId.HasValue)
                {
                    ModelState.AddModelError("", "Please select a doctor.");
                    await LoadViewData();
                    return View(model);
                }

                var validMedications = model.ScriptLines?.Where(sl => sl.MedicationId > 0).ToList();
                if (validMedications == null || !validMedications.Any())
                {
                    ModelState.AddModelError("", "Please add at least one medication.");
                    await LoadViewData();
                    return View(model);
                }

                // Validate script lines before processing
                var validationErrors = await ValidateScriptLinesBeforeSave(validMedications);
                if (validationErrors.Any())
                {
                    TempData["ErrorMessage"] = string.Join(" ", validationErrors);
                    await LoadViewData();
                    return View(model);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // 1. Create Prescription with "WalkIn" status
                    var prescription = new Prescription
                    {
                        ApplicationUserId = await GetCustomerUserId(model.CustomerId.Value),
                        DoctorID = model.DoctorId.Value,
                        DateIssued = model.PrescriptionDate,
                        DispenseUponApproval = model.DispenseUponApproval,
                        Status = "WalkIn"
                    };

                    // Store PDF if uploaded
                    if (model.PrescriptionFile != null && model.PrescriptionFile.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await model.PrescriptionFile.CopyToAsync(ms);
                        prescription.Script = ms.ToArray();
                    }

                    _context.Prescriptions.Add(prescription);
                    await _context.SaveChangesAsync(); // Get PrescriptionID

                    // 2. Create ScriptLines with status management (Approved/Rejected)
                    foreach (var scriptLineVM in validMedications)
                    {
                        var scriptLine = new ScriptLine
                        {
                            PrescriptionID = prescription.PrescriptionID,
                            MedicationID = scriptLineVM.MedicationId,
                            Quantity = scriptLineVM.Quantity,
                            Instructions = scriptLineVM.Instructions,
                            Repeats = scriptLineVM.IsRepeat ? 3 : 0, // Default 3 repeats if enabled
                            RepeatsLeft = scriptLineVM.RepeatsLeft,
                            Status = scriptLineVM.Status ?? "Approved", // Use selected status
                            RejectionReason = scriptLineVM.RejectionReason
                        };

                        // Set approval/rejection dates based on status
                        UpdateScriptLineDates(scriptLine, scriptLineVM.Status);

                        _context.ScriptLines.Add(scriptLine);
                    }
                    await _context.SaveChangesAsync();

                    // 3. Create Order with "Ordered" status - but only for APPROVED medications
                    var approvedScriptLines = prescription.scriptLines.Where(sl => sl.Status == "Approved").ToList();

                    Order? order = null;
                    if (approvedScriptLines.Any())
                    {
                        order = await CreateOrderFromPrescription(prescription.PrescriptionID, model.CustomerId.Value);
                        await _context.SaveChangesAsync();
                    }

                    // Update prescription status based on script line statuses
                    UpdatePrescriptionStatus(prescription);

                    await _context.SaveChangesAsync();

                    // ADDED: Send appropriate email notification
                    await SendWalkInPrescriptionEmail(prescription, order, model.CustomerId.Value);

                    await transaction.CommitAsync();

                    var approvedCount = approvedScriptLines.Count;
                    var rejectedCount = prescription.scriptLines.Count(sl => sl.Status == "Rejected");

                    if (approvedCount > 0 && rejectedCount > 0)
                    {
                        TempData["SuccessMessage"] = $"Walk-in prescription created successfully! {approvedCount} medication(s) approved and order created. {rejectedCount} medication(s) rejected.";
                    }
                    else if (approvedCount > 0)
                    {
                        TempData["SuccessMessage"] = $"Walk-in prescription created successfully! All {approvedCount} medication(s) approved and order {order?.OrderNumber} is ready for dispensing.";
                    }
                    else
                    {
                        TempData["WarningMessage"] = $"Prescription created but all medications were rejected. No order was created.";
                    }

                    return RedirectToAction("Index", "PharmacistDispensing");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["ErrorMessage"] = $"Error creating walk-in prescription: {ex.Message}";
                    await LoadViewData();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                await LoadViewData();
                return View(model);
            }
        }

        private async Task<Order> CreateOrderFromPrescription(int prescriptionId, int customerId)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.scriptLines)
                    .ThenInclude(sl => sl.Medications)
                .FirstOrDefaultAsync(p => p.PrescriptionID == prescriptionId);

            if (prescription == null) throw new Exception("Prescription not found");

            // Generate order number
            string orderNumber = await GenerateUniqueOrderNumber();

            var order = new Order
            {
                CustomerID = customerId,
                OrderDate = DateTime.Now,
                Status = "Ordered", // Order status is "Ordered"
                VAT = 15,
                OrderNumber = orderNumber
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Get OrderID

            decimal subtotal = 0;

            // Create order lines with "Pending" status - ONLY for approved script lines
            foreach (var scriptLine in prescription.scriptLines.Where(sl => sl.Status == "Approved"))
            {
                var orderLine = new OrderLine
                {
                    OrderID = order.OrderID,
                    ScriptLineID = scriptLine.ScriptLineID,
                    MedicationID = scriptLine.MedicationID,
                    Quantity = scriptLine.Quantity,
                    ItemPrice = (int)scriptLine.Medications.CurrentPrice,
                    Status = "Pending" // OrderLine status is "Pending"
                };

                _context.OrderLines.Add(orderLine);
                subtotal += scriptLine.Medications.CurrentPrice * scriptLine.Quantity;

                // DO NOT update inventory here - wait for dispensing in PharmacistDispensingController
            }

            order.TotalDue = (subtotal + (subtotal * order.VAT / 100)).ToString("F2");
            return order;
        }

        // NEW: Validate script lines before saving
        private async Task<List<string>> ValidateScriptLinesBeforeSave(List<WalkInScriptLineVM> scriptLines)
        {
            var errors = new List<string>();

            if (scriptLines == null || !scriptLines.Any())
            {
                errors.Add("No medication lines to process.");
                return errors;
            }

            var validMedicationIds = await _context.Medications.Select(m => m.MedcationID).ToListAsync();

            foreach (var scriptLine in scriptLines.Where(sl => sl.MedicationId > 0))
            {
                if (!validMedicationIds.Contains(scriptLine.MedicationId))
                {
                    errors.Add($"Medication ID {scriptLine.MedicationId} does not exist in database.");
                    continue;
                }

                // Validate rejection reason if status is Rejected
                if (scriptLine.Status == "Rejected" && string.IsNullOrWhiteSpace(scriptLine.RejectionReason))
                {
                    errors.Add("Rejection reason is required for rejected medications.");
                }
            }

            return errors;
        }

        // NEW: Update script line dates based on status (from ScriptsProcessedController)
        private void UpdateScriptLineDates(ScriptLine scriptLine, string status)
        {
            if (status == "Approved")
            {
                scriptLine.ApprovedDate = DateTime.Now;
                scriptLine.RejectedDate = null;
            }
            else if (status == "Rejected")
            {
                scriptLine.RejectedDate = DateTime.Now;
                scriptLine.ApprovedDate = null;
            }
            else
            {
                scriptLine.ApprovedDate = null;
                scriptLine.RejectedDate = null;
            }
        }

        // NEW: Update overall prescription status based on script line statuses
        // UPDATED: Prescription stays as "WalkIn" unless ALL medications are rejected
        private void UpdatePrescriptionStatus(Prescription prescription)
        {
            var approvedLines = prescription.scriptLines.Count(sl => sl.Status == "Approved");
            var rejectedLines = prescription.scriptLines.Count(sl => sl.Status == "Rejected");
            var totalLines = prescription.scriptLines.Count;

            // Only change status if ALL medications are rejected
            if (rejectedLines == totalLines && totalLines > 0)
            {
                prescription.Status = "Rejected";
            }
            else
            {
                // Otherwise, keep it as "WalkIn" regardless of approval mix
                prescription.Status = "WalkIn";
            }
        }

        // =========================================================================
        // ADDED: EMAIL METHODS FOR WALK-IN PRESCRIPTIONS
        // =========================================================================

        /// <summary>
        /// Send welcome email with login credentials to new patients
        /// </summary>
        private async Task SendWelcomeEmail(Customer customer, string password)
        {
            try
            {
                if (customer?.ApplicationUser == null || string.IsNullOrEmpty(customer.ApplicationUser.Email))
                    return;

                var receiver = customer.ApplicationUser.Email;
                var customerName = $"{customer.ApplicationUser.Name} {customer.ApplicationUser.Surname}";
                var subject = "🏥 Welcome to Ibhayi Pharmacy - Your Account Details";

                var message = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.1);'>
                <!-- Header -->
                <div style='background: linear-gradient(135deg, #22586A, #3498db); padding: 30px; text-align: center; color: white;'>
                    <h1 style='margin: 0; font-size: 28px; font-weight: bold;'>🏥 Ibhayi Pharmacy</h1>
                    <p style='margin: 10px 0 0 0; font-size: 16px; opacity: 0.9;'>Your Trusted Healthcare Partner</p>
                </div>

                <!-- Content -->
                <div style='padding: 30px;'>
                    <h2 style='color: #22586A; margin-top: 0;'>Welcome, {customerName}!</h2>
                    
                    <p style='font-size: 16px; line-height: 1.6; color: #333;'>
                        Thank you for registering with Ibhayi Pharmacy. Your account has been successfully created 
                        and you can now access our online services.
                    </p>

                    <!-- Credentials Box -->
                    <div style='background-color: #f8f9fa; border-left: 4px solid #3498db; padding: 20px; border-radius: 6px; margin: 25px 0;'>
                        <h3 style='color: #22586A; margin-top: 0;'>🔐 Your Login Credentials</h3>
                        
                        <div style='background-color: white; padding: 15px; border-radius: 5px; border: 1px solid #e1e8ed; margin: 15px 0;'>
                            <table style='width: 100%; border-collapse: collapse;'>
                                <tr>
                                    <td style='padding: 8px 0; border-bottom: 1px solid #f8f9fa;'><strong>Username:</strong></td>
                                    <td style='padding: 8px 0; border-bottom: 1px solid #f8f9fa; color: #22586A; font-weight: bold;'>{customer.ApplicationUser.Email}</td>
                                </tr>
                                <tr>
                                    <td style='padding: 8px 0;'><strong>Password:</strong></td>
                                    <td style='padding: 8px 0; color: #22586A; font-weight: bold;'>{password}</td>
                                </tr>
                            </table>
                        </div>

                        <div style='background-color: #fff3cd; padding: 12px; border-radius: 4px; border-left: 4px solid #ffc107; margin: 15px 0;'>
                            <p style='margin: 0; color: #856404; font-size: 14px;'>
                                <strong>🔒 Security Notice:</strong> For your security, we recommend changing your password after first login.
                            </p>
                        </div>
                    </div>

                    <!-- Features -->
                    <div style='margin: 25px 0;'>
                        <h3 style='color: #22586A;'>📋 What You Can Do With Your Account:</h3>
                        <ul style='color: #333; line-height: 1.8;'>
                            <li>View your prescription history</li>
                            <li>Track your medication orders</li>
                            <li>Receive important health notifications</li>
                            <li>Access your allergy information</li>
                            <li>Get prescription status updates</li>
                        </ul>
                    </div>

                    <!-- Next Steps -->
                    <div style='background-color: #e8f4fd; padding: 20px; border-radius: 6px; border: 1px solid #3498db;'>
                        <h3 style='color: #2980b9; margin-top: 0;'>🚀 Getting Started</h3>
                        <ol style='color: #333; line-height: 1.8;'>
                            <li>Visit our pharmacy portal</li>
                            <li>Login using the credentials above</li>
                            <li>Update your profile if needed</li>
                            <li>Explore your account features</li>
                        </ol>
                    </div>

                    <!-- Support -->
                    <div style='text-align: center; margin-top: 30px; padding-top: 20px; border-top: 2px solid #f8f9fa;'>
                        <p style='color: #7f8c8d; font-size: 14px;'>
                            Need help? Contact our support team:<br>
                            📞 <strong>041 123 4567</strong> | ✉️ <strong>support@ibhayipharmacy.co.za</strong>
                        </p>
                    </div>
                </div>

                <!-- Footer -->
                <div style='background-color: #2c3e50; padding: 20px; text-align: center; color: #ecf0f1;'>
                    <p style='margin: 0; font-size: 14px;'>
                        &copy; 2024 Ibhayi Pharmacy. All rights reserved.<br>
                        <small>Delivering Quality Healthcare to Our Community</small>
                    </p>
                </div>
            </div>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch (Exception ex)
            {
                // Log but don't break the registration process
                Console.WriteLine($"Welcome email failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Send email notification for walk-in prescription creation
        /// </summary>
        private async Task SendWalkInPrescriptionEmail(Prescription prescription, Order? order, int customerId)
        {
            try
            {
                // Load customer and doctor details for email
                var customer = await _context.Customers
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(c => c.CustormerID == customerId);

                var doctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.DoctorID == prescription.DoctorID);

                if (customer?.ApplicationUser == null || string.IsNullOrEmpty(customer.ApplicationUser.Email))
                    return;

                var receiver = customer.ApplicationUser.Email;
                var customerName = $"{customer.ApplicationUser.Name} {customer.ApplicationUser.Surname}";
                var doctorName = doctor != null ? $"Dr. {doctor.Name} {doctor.Surname}" : "Unknown Doctor";

                // Get medication details for email
                var scriptLines = await _context.ScriptLines
                    .Where(sl => sl.PrescriptionID == prescription.PrescriptionID)
                    .Include(sl => sl.Medications)
                    .ToListAsync();

                var approvedMeds = new List<string>();
                var rejectedMeds = new List<string>();
                decimal totalApprovedAmount = 0;

                foreach (var sl in scriptLines)
                {
                    var medName = sl.Medications?.MedicationName ?? "Unknown Medication";
                    var instructions = sl.Instructions ?? "Take as directed";
                    var price = sl.Medications?.CurrentPrice ?? 0;
                    var lineTotal = price * sl.Quantity;

                    if (sl.Status == "Approved")
                    {
                        totalApprovedAmount += lineTotal;
                        approvedMeds.Add($@"
                            <div style='background-color: #e8f5e8; border-left: 4px solid #27ae60; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                                <strong style='color: #27ae60; font-size: 16px;'>🟢 {medName}</strong><br>
                                <strong>Quantity:</strong> {sl.Quantity} | <strong>Price:</strong> R {price:F2} each<br>
                                <strong>Line Total:</strong> R {lineTotal:F2}<br>
                                <strong>Instructions:</strong> {instructions}
                            </div>");
                    }
                    else if (sl.Status == "Rejected")
                    {
                        rejectedMeds.Add($@"
                            <div style='background-color: #ffeaea; border-left: 4px solid #e74c3c; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                                <strong style='color: #e74c3c;'>🔴 {medName}</strong><br>
                                <strong>Reason:</strong> {sl.RejectionReason ?? "Not specified"}<br>
                                <strong>Instructions:</strong> {instructions}
                            </div>");
                    }
                }

                var subject = "";
                var message = "";

                if (order != null && approvedMeds.Any())
                {
                    // Order created successfully
                    subject = $"💊 Walk-In Prescription Processed - {order.OrderNumber} - IbhayiPharmacy-GRP-04-14";

                    message = $@"
                        <h3>💊 Your Walk-In Prescription Has Been Processed</h3>
                        <p>Dear {customerName}, your walk-in prescription has been processed successfully!</p>
                        
                        <div style='background-color: #f0f8ff; padding: 20px; border-radius: 8px; margin: 20px 0; border: 2px solid #3498db;'>
                            <h4 style='color: #3498db; margin-top: 0;'>📦 Order Created</h4>
                            <p><strong>Order Number:</strong> {order.OrderNumber}</p>
                            <p><strong>Prescription Date:</strong> {prescription.DateIssued:dd MMMM yyyy}</p>
                            <p><strong>Prescribing Doctor:</strong> {doctorName}</p>
                            <p><strong>Approved Amount:</strong> <strong style='color: #27ae60;'>R {totalApprovedAmount:F2}</strong></p>
                            <p><strong>Total Order Amount:</strong> R {order.TotalDue}</p>
                        </div>";

                    if (approvedMeds.Any())
                    {
                        message += @"<h4 style='color: #27ae60;'>✅ MEDICATIONS APPROVED & READY FOR DISPENSING:</h4>";
                        foreach (var med in approvedMeds)
                        {
                            message += med;
                        }
                    }

                    if (rejectedMeds.Any())
                    {
                        message += @"<h4 style='color: #e74c3c;'>❌ MEDICATIONS NOT APPROVED:</h4>";
                        foreach (var med in rejectedMeds)
                        {
                            message += med;
                        }

                        message += $@"
                            <div style='background-color: #fff3cd; padding: 15px; border-radius: 5px; margin: 15px 0; border-left: 4px solid #f39c12;'>
                                <h4 style='color: #856404; margin-top: 0;'>👨‍⚕️ Important Notice</h4>
                                <p>For medications that could not be approved, please <strong>consult your doctor</strong> to discuss alternative treatment options.</p>
                            </div>";
                    }

                    message += $@"
                        <div style='background-color: #e8f4fd; padding: 18px; border-radius: 6px; margin: 20px 0; border: 1px solid #3498db;'>
                            <h4 style='color: #2980b9; margin-top: 0;'>🛎️ WHAT HAPPENS NEXT</h4>
                            <ul style='margin-bottom: 0;'>
                                <li>Your order is now in our dispensing queue</li>
                                <li>You will receive another email when your medications are ready for collection</li>
                                <li>Collection available during pharmacy hours</li>
                                <li>Please bring your ID document when collecting</li>
                            </ul>
                        </div>";
                }
                else
                {
                    // All medications rejected - no order created
                    subject = $"⚠️ Walk-In Prescription Update - IbhayiPharmacy-GRP-04-14";

                    message = $@"
                        <h3>⚠️ Walk-In Prescription Update</h3>
                        <p>Dear {customerName}, we need to discuss your recent walk-in prescription.</p>
                        
                        <div style='background-color: #fffaf0; padding: 20px; border-radius: 8px; margin: 20px 0; border: 2px solid #e67e22;'>
                            <h4 style='color: #e67e22; margin-top: 0;'>📋 Prescription Details</h4>
                            <p><strong>Prescription Date:</strong> {prescription.DateIssued:dd MMMM yyyy}</p>
                            <p><strong>Prescribing Doctor:</strong> {doctorName}</p>
                            <p><strong>Status:</strong> <strong style='color: #e74c3c;'>Requires Follow-up</strong></p>
                        </div>";

                    if (rejectedMeds.Any())
                    {
                        message += @"<h4 style='color: #e74c3c;'>❌ MEDICATIONS NOT APPROVED:</h4>";
                        foreach (var med in rejectedMeds)
                        {
                            message += med;
                        }
                    }

                    message += $@"
                        <div style='background-color: #f8d7da; padding: 18px; border-radius: 6px; margin: 20px 0; border: 1px solid #e74c3c;'>
                            <h4 style='color: #721c24; margin-top: 0;'>👨‍⚕️ URGENT: CONSULT YOUR DOCTOR</h4>
                            <p><strong>None of your prescribed medications could be approved at this time.</strong></p>
                            <p>Please <strong>consult your doctor immediately</strong> to discuss:</p>
                            <ul style='margin-bottom: 0;'>
                                <li>Alternative medication options</li>
                                <li>New prescriptions if needed</li>
                                <li>Different treatment approaches</li>
                            </ul>
                        </div>";
                }

                message += $@"
                    <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                        <h4 style='color: #2c3e50; margin-top: 0;'>📍 PHARMACY INFORMATION</h4>
                        <p style='margin-bottom: 5px;'><strong>Ibhayi Pharmacy</strong></p>
                        <p style='margin-bottom: 5px;'>123 Govan Mbeki Avenue</p>
                        <p style='margin-bottom: 5px;'>Port Elizabeth, 6001</p>
                        <p style='margin-bottom: 0;'>📞 041 123 4567</p>
                    </div>

                    <p>Thank you for choosing Ibhayi Pharmacy! 🏥</p>
                    
                    <div style='border-top: 2px solid #3498db; padding-top: 15px; margin-top: 20px;'>
                        <p style='margin-bottom: 5px;'>Regards,</p>
                        <p style='margin-bottom: 5px; font-weight: bold;'>The Pharmacy Team</p>
                        <p style='margin-bottom: 0; font-weight: bold; color: #3498db;'>Ibhayi Pharmacy</p>
                    </div>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch (Exception)
            {
                // Email failure shouldn't break the prescription creation process
            }
        }

        // AJAX: Search customers
        [HttpGet]
        public async Task<JsonResult> SearchCustomers(string searchTerm)
        {
            try
            {
                var query = _context.Customers
                    .Include(c => c.ApplicationUser)
                    .AsQueryable();

                // Only apply filter if searchTerm is not empty
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(c => c.ApplicationUser.Name.Contains(searchTerm) ||
                                           c.ApplicationUser.Surname.Contains(searchTerm) ||
                                           c.ApplicationUser.IDNumber.Contains(searchTerm));
                }

                var customers = await query
                    .Select(c => new
                    {
                        id = c.CustormerID,
                        text = $"{c.ApplicationUser.Name} {c.ApplicationUser.Surname} (ID: {c.ApplicationUser.IDNumber})",
                        name = c.ApplicationUser.Name,
                        surname = c.ApplicationUser.Surname,
                        idNumber = c.ApplicationUser.IDNumber
                    })
                    .Take(20)
                    .ToListAsync();

                return Json(customers);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // AJAX: Search doctors  
        [HttpGet]
        public async Task<JsonResult> SearchDoctors(string searchTerm)
        {
            try
            {
                var query = _context.Doctors.AsQueryable();

                // Only apply filter if searchTerm is not empty
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(d => d.Name.Contains(searchTerm) ||
                                           d.Surname.Contains(searchTerm) ||
                                           d.HealthCouncilRegistrationNumber.Contains(searchTerm));
                }

                var doctors = await query
                    .Select(d => new
                    {
                        id = d.DoctorID,
                        text = $"Dr. {d.Name} {d.Surname} ({d.HealthCouncilRegistrationNumber})",
                        name = d.Name,
                        surname = d.Surname,
                        practiceNumber = d.HealthCouncilRegistrationNumber
                    })
                    .Take(20)
                    .ToListAsync();

                return Json(doctors);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // AJAX: Search medications
        [HttpGet]
        public async Task<JsonResult> SearchMedications(string searchTerm)
        {
            try
            {
                var query = _context.Medications
                    .Include(m => m.DosageForm)
                    .Include(m => m.Medication_Ingredients)
                        .ThenInclude(mi => mi.Active_Ingredients)
                    .AsQueryable();

                // Only apply filter if searchTerm is not empty
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(m => m.MedicationName.Contains(searchTerm));
                }

                var medications = await query
                    .Select(m => new
                    {
                        id = m.MedcationID,
                        name = m.MedicationName,
                        dosageForm = m.DosageForm.DosageFormName,
                        displayName = $"{m.MedicationName} ({m.DosageForm.DosageFormName})",
                        schedule = m.Schedule,
                        price = m.CurrentPrice,
                        stock = m.QuantityOnHand,
                        reorderLevel = m.ReOrderLevel,
                        activeIngredients = m.Medication_Ingredients.Select(mi =>
                            $"{mi.Active_Ingredients.Name} {mi.Strength}").ToList()
                    })
                    .Take(20)
                    .ToListAsync();

                return Json(medications);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // AJAX: Get allergy options from Active_Ingredients table
        [HttpGet]
        public async Task<JsonResult> GetAllergyOptions()
        {
            try
            {
                var allergies = await _context.Active_Ingredients
                    .Select(a => new
                    {
                        id = a.Active_IngredientID,
                        name = a.Name
                    })
                    .ToListAsync();

                return Json(allergies);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // AJAX: Get customer allergies from Customer_Allergies table
        [HttpGet]
        public async Task<JsonResult> GetCustomerAllergies(int customerId)
        {
            try
            {
                var allergies = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == customerId)
                    .Include(ca => ca.Active_Ingredient)
                    .Select(ca => new
                    {
                        id = ca.Active_Ingredient.Active_IngredientID,
                        name = ca.Active_Ingredient.Name
                    })
                    .ToListAsync();

                return Json(allergies);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // AJAX: Register new patient with real allergy system
        [HttpPost]
        public async Task<JsonResult> RegisterNewPatient([FromBody] RegisterPatientRequest request)
        {
            try
            {
                // Create ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    IDNumber = request.IDNumber,
                    CellphoneNumber = request.Cellphone
                };

                // Create user with password
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

                _context.ApplicationUsers.Add(user);
                await _context.SaveChangesAsync();

                // Assign to Customer role
                if (!await _roleManager.RoleExistsAsync("Customer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                }
                await _userManager.AddToRoleAsync(user, "Customer");

                // Create Customer record
                var customer = new Customer
                {
                    ApplicationUserId = user.Id
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync(); // Get CustomerID

                // Add allergies to Customer_Allergies table
                foreach (var allergyId in request.SelectedAllergyIds)
                {
                    var customerAllergy = new Custormer_Allergy
                    {
                        CustomerID = customer.CustormerID,
                        Active_IngredientID = allergyId
                    };
                    _context.Custormer_Allergies.Add(customerAllergy);
                }

                await _context.SaveChangesAsync();

                // ✅ ADDED: Send welcome email with credentials
                await SendWelcomeEmail(customer, request.Password);

                return Json(new
                {
                    success = true,
                    customerId = customer.CustormerID,
                    customerName = $"{user.Name} {user.Surname}",
                    customerIDNumber = user.IDNumber,
                    message = "Patient registered successfully"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // AJAX: Register new doctor
        [HttpPost]
        public async Task<JsonResult> RegisterNewDoctor([FromBody] RegisterDoctorRequest request)
        {
            try
            {
                var doctor = new Doctor
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    HealthCouncilRegistrationNumber = request.PracticeNumber,
                    ContactNumber = request.ContactNumber,
                    Email = request.Email
                };

                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    doctorId = doctor.DoctorID,
                    doctorName = $"Dr. {doctor.Name} {doctor.Surname}",
                    practiceNumber = doctor.HealthCouncilRegistrationNumber,
                    message = "Doctor registered successfully"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // AJAX: Check allergy conflicts against medication ingredients - FIXED VERSION
        [HttpGet]
        public async Task<JsonResult> CheckAllergyConflicts(int customerId, int medicationId)
        {
            try
            {
                // Get customer allergies (Active_Ingredient IDs)
                var customerAllergyIds = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == customerId)
                    .Select(ca => ca.Active_IngredientID)
                    .ToListAsync();

                // Get medication ingredients (Active_Ingredient IDs)
                var medicationIngredientIds = await _context.Medication_Ingredients
                    .Where(mi => mi.MedicationID == medicationId)
                    .Select(mi => mi.Active_IngredientID)
                    .ToListAsync();

                // Find conflicts by comparing IDs (exact match)
                var conflictIds = customerAllergyIds
                    .Where(allergyId => medicationIngredientIds.Contains(allergyId))
                    .ToList();

                // Get the names of conflicting ingredients
                var conflictNames = await _context.Active_Ingredients
                    .Where(ai => conflictIds.Contains(ai.Active_IngredientID))
                    .Select(ai => ai.Name)
                    .ToListAsync();

                return Json(new
                {
                    hasConflicts = conflictNames.Any(),
                    conflicts = conflictNames
                });
            }
            catch (Exception ex)
            {
                return Json(new { hasConflicts = false, conflicts = new string[0] });
            }
        }

        // AJAX: Get medication details
        [HttpGet]
        public async Task<JsonResult> GetMedicationDetails(int medicationId)
        {
            try
            {
                var medication = await _context.Medications
                    .Include(m => m.Medication_Ingredients)
                        .ThenInclude(mi => mi.Active_Ingredients)
                    .Include(m => m.DosageForm)
                    .FirstOrDefaultAsync(m => m.MedcationID == medicationId);

                if (medication == null)
                    return Json(new { error = "Medication not found" });

                var activeIngredients = medication.Medication_Ingredients?
                    .Select(mi => $"{mi.Active_Ingredients?.Name} {mi.Strength}")
                    .ToList() ?? new List<string>();

                var isLowStock = medication.QuantityOnHand <= medication.ReOrderLevel + 10;

                return Json(new
                {
                    medicationName = medication.MedicationName,
                    activeIngredients = string.Join(", ", activeIngredients),
                    stock = medication.QuantityOnHand,
                    reorderLevel = medication.ReOrderLevel,
                    dosageForm = medication.DosageForm?.DosageFormName ?? "Unknown",
                    schedule = medication.Schedule,
                    price = medication.CurrentPrice,
                    isLowStock = isLowStock
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Error loading medication: {ex.Message}" });
            }
        }

        private async Task<string> GetCustomerUserId(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.CustormerID == customerId);

            return customer?.ApplicationUserId ?? throw new Exception("Customer not found");
        }

        private async Task<string> GenerateUniqueOrderNumber()
        {
            string orderNumber;
            bool isUnique;
            int maxAttempts = 5;
            int attempts = 0;

            do
            {
                // Format: ORD-W-RRRR (RRRR = 4 random digits)
                string randomPart = _random.Next(1000, 10000).ToString();
                orderNumber = $"ORD-W-{randomPart}";

                isUnique = !await _context.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
                attempts++;

            } while (!isUnique && attempts < maxAttempts);

            // Fallback if we still don't have a unique number
            if (!isUnique)
            {
                string timestamp = DateTime.Now.ToString("HHmmss");
                orderNumber = $"ORD-W-{timestamp}";
            }

            return orderNumber;
        }

        private async Task LoadViewData()
        {
            // Preload any needed data for dropdowns
            ViewBag.DosageForms = await _context.DosageForms.ToListAsync();
        }
    }

    // Request models (keep these exactly as they are)
    public class RegisterPatientRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IDNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<int> SelectedAllergyIds { get; set; } = new List<int>();
    }

    public class RegisterDoctorRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PracticeNumber { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}