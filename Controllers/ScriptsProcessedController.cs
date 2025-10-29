using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Pharmacist")]
    public class ScriptsProcessedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new Random();
        private readonly EmailService _email;

        public ScriptsProcessedController(ApplicationDbContext context, EmailService email)
        {
            _context = context;
            _email = email;
        }

        // GET: Index - Show unprocessed prescriptions
        public async Task<IActionResult> Index()
        {
            try
            {
                var unprocessedPrescriptions = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Doctors)
                    .Where(p => string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending")
                    .OrderByDescending(p => p.DateIssued)
                    .ToListAsync();

                return View(unprocessedPrescriptions);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading prescriptions: {ex.Message}";
                return View(new List<Prescription>());
            }
        }

        // GET: Edit - Show form to process prescription (regular processing)
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Doctors)
                    .Include(p => p.scriptLines)
                    .ThenInclude(sl => sl.Medications)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id);

                if (prescription == null)
                {
                    TempData["ErrorMessage"] = "Prescription not found";
                    return RedirectToAction(nameof(Index));
                }

                var customer = await _context.Customers
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == prescription.ApplicationUserId);

                if (customer == null)
                {
                    TempData["ErrorMessage"] = "Customer not found for this prescription";
                    return RedirectToAction(nameof(Index));
                }

                var customerAllergies = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == customer.CustormerID)
                    .Include(ca => ca.Active_Ingredient)
                    .Select(ca => ca.Active_Ingredient.Name)
                    .ToListAsync();

                var viewModel = new CustomerScriptsVM
                {
                    Prescr = prescription.PrescriptionID,
                    Name = customer.ApplicationUser?.Name ?? "",
                    Surname = customer.ApplicationUser?.Surname ?? "",
                    IDNumber = customer.ApplicationUser?.IDNumber ?? "",
                    PrescriptionDate = prescription.DateIssued,
                    CustomerAllergies = customerAllergies,
                    ScriptList = new List<Prescription> { prescription },
                    DoctorId = prescription.DoctorID,
                    DoctorName = prescription.Doctors != null ?
                        $"{prescription.Doctors.Name} {prescription.Doctors.Surname}" : "",
                    ScriptLines = prescription.scriptLines.Select(sl => new ScriptLineVM
                    {
                        ScriptLineId = sl.ScriptLineID,
                        MedicationId = sl.MedicationID,
                        MedicationName = sl.Medications?.MedicationName ?? "",
                        Quantity = sl.Quantity,
                        Instructions = sl.Instructions ?? "",
                        IsRepeat = sl.Repeats > 0,
                        RepeatsLeft = sl.RepeatsLeft,
                        Status = sl.Status ?? "Pending",
                        RejectionReason = sl.RejectionReason,
                        CanBeApproved = prescription.DoctorID.HasValue
                    }).ToList()
                };

                if (!viewModel.ScriptLines.Any())
                {
                    viewModel.ScriptLines.Add(new ScriptLineVM
                    {
                        ScriptLineId = 0,
                        Status = "Pending",
                        Quantity = 1,
                        RepeatsLeft = 0,
                        IsRepeat = false,
                        Instructions = "",
                        CanBeApproved = prescription.DoctorID.HasValue
                    });
                }

                await ReloadViewBags();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading prescription: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProcessAndDispense - Show form to process prescription with automatic dispensing
        public async Task<IActionResult> ProcessAndDispense(int id)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Doctors)
                    .Include(p => p.scriptLines)
                    .ThenInclude(sl => sl.Medications)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id);

                if (prescription == null)
                {
                    TempData["ErrorMessage"] = "Prescription not found";
                    return RedirectToAction(nameof(Index));
                }

                var customer = await _context.Customers
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == prescription.ApplicationUserId);

                if (customer == null)
                {
                    TempData["ErrorMessage"] = "Customer not found for this prescription";
                    return RedirectToAction(nameof(Index));
                }

                var customerAllergies = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == customer.CustormerID)
                    .Include(ca => ca.Active_Ingredient)
                    .Select(ca => ca.Active_Ingredient.Name)
                    .ToListAsync();

                var viewModel = new CustomerScriptsVM
                {
                    Prescr = prescription.PrescriptionID,
                    Name = customer.ApplicationUser?.Name ?? "",
                    Surname = customer.ApplicationUser?.Surname ?? "",
                    IDNumber = customer.ApplicationUser?.IDNumber ?? "",
                    PrescriptionDate = prescription.DateIssued,
                    CustomerAllergies = customerAllergies,
                    ScriptList = new List<Prescription> { prescription },
                    DoctorId = prescription.DoctorID,
                    DoctorName = prescription.Doctors != null ?
                        $"{prescription.Doctors.Name} {prescription.Doctors.Surname}" : "",
                    ScriptLines = prescription.scriptLines.Select(sl => new ScriptLineVM
                    {
                        ScriptLineId = sl.ScriptLineID,
                        MedicationId = sl.MedicationID,
                        MedicationName = sl.Medications?.MedicationName ?? "",
                        Quantity = sl.Quantity,
                        Instructions = sl.Instructions ?? "",
                        IsRepeat = sl.Repeats > 0,
                        RepeatsLeft = sl.RepeatsLeft,
                        Status = sl.Status ?? "Pending",
                        RejectionReason = sl.RejectionReason,
                        CanBeApproved = prescription.DoctorID.HasValue
                    }).ToList()
                };

                if (!viewModel.ScriptLines.Any())
                {
                    viewModel.ScriptLines.Add(new ScriptLineVM
                    {
                        ScriptLineId = 0,
                        Status = "Pending",
                        Quantity = 1,
                        RepeatsLeft = 0,
                        IsRepeat = false,
                        Instructions = "",
                        CanBeApproved = prescription.DoctorID.HasValue
                    });
                }

                await ReloadViewBags();
                return View("ProcessAndDispense", viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading prescription: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Edit - Save to database (regular processing)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerScriptsVM model)
        {
            return await ProcessPrescriptionInternal(model, false);
        }

        // POST: ProcessAndDispense - Save to database and create order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessAndDispense(CustomerScriptsVM model)
        {
            if (!model.DoctorId.HasValue || model.DoctorId == 0)
            {
                var doctorIdValue = Request.Form["DoctorId"].FirstOrDefault();
                if (!string.IsNullOrEmpty(doctorIdValue) && int.TryParse(doctorIdValue, out int doctorId))
                {
                    model.DoctorId = doctorId;
                }
            }

            return await ProcessPrescriptionInternal(model, true);
        }

        // Internal method to handle both regular processing and process+dispense
        private async Task<IActionResult> ProcessPrescriptionInternal(CustomerScriptsVM model, bool createOrder)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .Include(p => p.scriptLines)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == model.Prescr);

                if (prescription == null)
                {
                    TempData["ErrorMessage"] = "Prescription not found";
                    return RedirectToAction(nameof(Index));
                }

                var hasMedications = model.ScriptLines?.Any(sl => sl.MedicationId > 0) == true;

                if (!hasMedications)
                {
                    ModelState.AddModelError("", "Please add at least one medication to process the prescription.");
                    TempData["ErrorMessage"] = "Please add at least one medication to process the prescription.";
                    await ReloadViewBags();
                    return View(createOrder ? "ProcessAndDispense" : "Edit", model);
                }

                var hasApprovedMedications = model.ScriptLines?.Any(sl => sl.Status == "Approved") == true;

                if (hasApprovedMedications && (!model.DoctorId.HasValue || model.DoctorId.Value == 0))
                {
                    ModelState.AddModelError("DoctorId", "Doctor is required when approving medications.");
                    TempData["ErrorMessage"] = "Please select a doctor before approving any medications.";
                    await ReloadViewBags();
                    return View(createOrder ? "ProcessAndDispense" : "Edit", model);
                }

                var validationErrors = await ValidateScriptLinesBeforeSave(model.ScriptLines);
                if (validationErrors.Any())
                {
                    TempData["ErrorMessage"] = string.Join(" ", validationErrors);
                    await ReloadViewBags();
                    return View(createOrder ? "ProcessAndDispense" : "Edit", model);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    if (model.DoctorId.HasValue && model.DoctorId.Value > 0)
                    {
                        prescription.DoctorID = model.DoctorId.Value;
                    }

                    foreach (var scriptLineVM in model.ScriptLines.Where(sl => sl.MedicationId > 0))
                    {
                        if (scriptLineVM.ScriptLineId > 0)
                        {
                            await UpdateExistingScriptLine(scriptLineVM);
                        }
                        else
                        {
                            await CreateNewScriptLine(scriptLineVM, model.Prescr);
                        }
                    }

                    // Save changes first to ensure script lines are persisted
                    await _context.SaveChangesAsync();

                    // Update prescription status after saving script lines
                    await UpdatePrescriptionStatus(model, prescription);

                    await _context.SaveChangesAsync();

                    if (createOrder && hasApprovedMedications)
                    {
                        var customer = await _context.Customers
                            .Include(c => c.ApplicationUser)
                            .FirstOrDefaultAsync(c => c.ApplicationUserId == prescription.ApplicationUserId);

                        if (customer != null)
                        {
                            var order = await CreateOrderFromPrescription(prescription.PrescriptionID, customer.CustormerID);

                            if (order != null)
                            {
                                SendOrderConfirmationEmail(customer, order, prescription);

                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();

                                TempData["SuccessMessage"] = $"Prescription processed successfully! Order {order.OrderNumber} created and ready for dispensing. Confirmation email sent to customer.";
                                return RedirectToAction("DispenseOrder", "PharmacistDispensing", new { id = order.OrderID });
                            }
                            else
                            {
                                TempData["WarningMessage"] = "Prescription processed but no order was created (no approved medications with valid data).";
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Customer not found for this prescription.";
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    TempData["SuccessMessage"] = "Prescription processed successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (DbUpdateException dbEx)
            {
                var errorMessage = "Database error occurred while saving changes.";
                if (dbEx.InnerException != null)
                {
                    var innerMessage = dbEx.InnerException.Message;
                    if (innerMessage.Contains("FK_Prescriptions_Doctors_DoctorID"))
                    {
                        errorMessage = "Error: Invalid Doctor ID. Please select a valid doctor from the list.";
                    }
                    else if (innerMessage.Contains("FK_ScriptLines_Medications_MedicationID"))
                    {
                        errorMessage = "Error: Invalid Medication ID. Please select valid medications.";
                    }
                    else
                    {
                        errorMessage += $" {innerMessage}";
                    }
                }

                TempData["ErrorMessage"] = errorMessage;
                await ReloadViewBags();
                return View(createOrder ? "ProcessAndDispense" : "Edit", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error processing prescription: {ex.Message}";
                await ReloadViewBags();
                return View(createOrder ? "ProcessAndDispense" : "Edit", model);
            }
        }

        // EMAIL METHOD: Send order confirmation to customer
        private void SendOrderConfirmationEmail(Customer customer, Order order, Prescription prescription)
        {
            try
            {
                var receiver = customer.ApplicationUser?.Email;
                if (string.IsNullOrEmpty(receiver))
                {
                    return;
                }

                var subject = $"Order Confirmation - {order.OrderNumber}";

                // Get approved medications for the email
                var approvedMedications = _context.ScriptLines
                    .Where(sl => sl.PrescriptionID == prescription.PrescriptionID && sl.Status == "Approved")
                    .Include(sl => sl.Medications)
                    .ToList();

                var message = $@"
                    <h3>Order Confirmation</h3>
                    <p>Dear {customer.ApplicationUser?.Name} {customer.ApplicationUser?.Surname},</p>
                    
                    <p>Your prescription has been processed and your order is ready for collection.</p>
                    
                    <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                        <h4>Order Details:</h4>
                        <p><strong>Order Number:</strong> {order.OrderNumber}</p>
                        <p><strong>Prescription Reference:</strong> Prescription_{prescription.PrescriptionID:D2}</p>
                        <p><strong>Order Date:</strong> {order.OrderDate:dd MMMM yyyy}</p>
                        <p><strong>Total Amount:</strong> R {order.TotalDue}</p>
                        <p><strong>Status:</strong> {order.Status}</p>
                    </div>";

                if (approvedMedications.Any())
                {
                    message += @"<h4>Approved Medications:</h4><ul>";

                    foreach (var medication in approvedMedications)
                    {
                        message += $@"<li><strong>{medication.Medications?.MedicationName}</strong> - Quantity: {medication.Quantity}<br>
                                    <small>Instructions: {medication.Instructions}</small></li>";
                    }

                    message += "</ul>";
                }
                else
                {
                    message += @"<p><em>No medications were approved for this order.</em></p>";
                }

                message += $@"
                    <p>Please bring your ID and medical aid card when collecting your medication.</p>
                    
                    <p><strong>Collection Address:</strong><br>
                    Ibhayi Pharmacy<br>
                    [Your Pharmacy Address]<br>
                    [Contact Number]</p>

                    <p>Thank you for choosing Ibhayi Pharmacy!</p>
                    <p>Best regards,<br>Ibhayi Pharmacy Team</p>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch (Exception ex)
            {
                // Email failure shouldn't break the process
            }
        }

        // EMAIL METHOD: Send prescription status update
        // EMAIL METHOD: Send prescription status update
        private void SendPrescriptionStatusEmail(Prescription prescription, string status)
        {
            try
            {
                var customer = _context.Customers
                    .Include(c => c.ApplicationUser)
                    .FirstOrDefault(c => c.ApplicationUserId == prescription.ApplicationUserId);

                if (customer?.ApplicationUser == null || string.IsNullOrEmpty(customer.ApplicationUser.Email))
                {
                    return;
                }

                var receiver = customer.ApplicationUser.Email;
                var subject = $"Prescription Update - {status}";

                // Get all script lines with medications
                var scriptLines = _context.ScriptLines
                    .Where(sl => sl.PrescriptionID == prescription.PrescriptionID)
                    .Include(sl => sl.Medications)
                    .ToList();

                string statusMessage = status switch
                {
                    "Processed" => "has been fully processed and is ready for collection.",
                    "Partially Processed" => "has been partially processed. Some medications are ready for collection.",
                    "Rejected" => "could not be processed. Please contact the pharmacy for more information.",
                    _ => "is being processed. We will notify you when it's ready."
                };

                var message = $@"
            <h3>Prescription Status Update</h3>
            <p>Dear {customer.ApplicationUser.Name} {customer.ApplicationUser.Surname},</p>
            
            <p>Your prescription dated {prescription.DateIssued:dd MMMM yyyy} {statusMessage}</p>
            
            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                <p><strong>Prescription Reference:</strong> Prescription_{prescription.PrescriptionID:D2}</p>
                <p><strong>Status:</strong> {status}</p>
                <p><strong>Date Processed:</strong> {DateTime.Now:dd MMMM yyyy HH:mm}</p>
            </div>";

                // Add medication details
                if (scriptLines.Any())
                {
                    message += @"<h4>Medication Details:</h4><ul>";

                    foreach (var scriptLine in scriptLines)
                    {
                        var statusBadge = scriptLine.Status == "Approved" ?
                            "<span style='color: #27ae60; font-weight: bold;'>✓ APPROVED</span>" :
                            scriptLine.Status == "Rejected" ?
                            "<span style='color: #e74c3c; font-weight: bold;'>✗ REJECTED</span>" :
                            "<span style='color: #f39c12; font-weight: bold;'>⏳ PENDING</span>";

                        message += $@"<li>
                        <strong>{scriptLine.Medications?.MedicationName}</strong> - {statusBadge}<br>
                        <small>Quantity: {scriptLine.Quantity} | Instructions: {scriptLine.Instructions}</small>";

                        // Add repeat information if applicable - BOLD BLUE
                        if (scriptLine.Repeats > 0 && scriptLine.Status == "Approved")
                        {
                            message += $@"<br><small style='color: #1e40af; font-weight: bold; background-color: #dbeafe; padding: 2px 6px; border-radius: 3px;'>🔄 REPEATS: {scriptLine.RepeatsLeft} out of {scriptLine.Repeats} remaining</small>";
                        }

                        // Add rejection reason if rejected
                        if (scriptLine.Status == "Rejected" && !string.IsNullOrEmpty(scriptLine.RejectionReason))
                        {
                            message += $@"<br><small style='color: #e74c3c;'><strong>Rejection Reason:</strong> {scriptLine.RejectionReason}</small>";
                        }

                        message += "</li>";
                    }

                    message += "</ul>";
                }

                message += $@"
            <p>If you have any questions, please don't hesitate to contact us.</p>
            
            <p>Best regards,<br>Ibhayi Pharmacy Team</p>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch (Exception ex)
            {
                // Email failure shouldn't break the process
            }
        }

        // Create order from prescription
        private async Task<Order> CreateOrderFromPrescription(int prescriptionId, int customerId)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .Include(p => p.scriptLines)
                        .ThenInclude(sl => sl.Medications)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == prescriptionId);

                if (prescription == null)
                {
                    throw new Exception("Prescription not found");
                }

                var approvedScriptLines = prescription.scriptLines.Where(sl => sl.Status == "Approved").ToList();

                if (!approvedScriptLines.Any())
                {
                    return null;
                }

                string orderNumber = await GenerateUniqueOrderNumber();

                var order = new Order
                {
                    CustomerID = customerId,
                    OrderDate = DateTime.Now,
                    Status = "Ordered",
                    VAT = 15,
                    OrderNumber = orderNumber
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                decimal subtotal = 0;
                bool hasValidMedications = false;

                foreach (var scriptLine in approvedScriptLines)
                {
                    if (scriptLine.Medications == null)
                    {
                        continue;
                    }

                    if (scriptLine.Medications.CurrentPrice <= 0)
                    {
                        continue;
                    }

                    var orderLine = new OrderLine
                    {
                        OrderID = order.OrderID,
                        ScriptLineID = scriptLine.ScriptLineID,
                        MedicationID = scriptLine.MedicationID,
                        Quantity = scriptLine.Quantity,
                        ItemPrice = (int)scriptLine.Medications.CurrentPrice,
                        Status = "Pending"
                    };

                    _context.OrderLines.Add(orderLine);
                    subtotal += scriptLine.Medications.CurrentPrice * scriptLine.Quantity;
                    hasValidMedications = true;
                }

                if (!hasValidMedications)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    return null;
                }

                decimal totalDue = subtotal + (subtotal * order.VAT / 100);
                order.TotalDue = totalDue.ToString("F2");

                return order;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Generate unique order number with ORD-P-RRRR format (P for Processed)
        private async Task<string> GenerateUniqueOrderNumber()
        {
            string orderNumber;
            bool isUnique;
            int maxAttempts = 5;
            int attempts = 0;

            do
            {
                string randomPart = _random.Next(1000, 10000).ToString();
                orderNumber = $"ORD-P-{randomPart}";

                isUnique = !await _context.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
                attempts++;

            } while (!isUnique && attempts < maxAttempts);

            if (!isUnique)
            {
                string timestamp = DateTime.Now.ToString("HHmmss");
                orderNumber = $"ORD-P-{timestamp}";
            }

            return orderNumber;
        }

        // POST: Add new doctor via AJAX
        [HttpPost]
        public async Task<JsonResult> AddNewDoctor([FromBody] Doctor newDoctor)
        {
            try
            {
                if (string.IsNullOrEmpty(newDoctor.Name) || string.IsNullOrEmpty(newDoctor.Surname) ||
                    string.IsNullOrEmpty(newDoctor.HealthCouncilRegistrationNumber) || string.IsNullOrEmpty(newDoctor.ContactNumber))
                {
                    return Json(new { success = false, message = "All fields are required" });
                }

                var existingDoctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.HealthCouncilRegistrationNumber == newDoctor.HealthCouncilRegistrationNumber);

                if (existingDoctor != null)
                {
                    return Json(new { success = false, message = "Doctor with this registration number already exists" });
                }

                _context.Doctors.Add(newDoctor);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    doctorId = newDoctor.DoctorID,
                    doctorName = $"{newDoctor.Name} {newDoctor.Surname}",
                    fullDisplay = $"{newDoctor.Name} {newDoctor.Surname} ({newDoctor.HealthCouncilRegistrationNumber})"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error saving doctor: {ex.Message}" });
            }
        }

        private async Task<List<string>> ValidateScriptLinesBeforeSave(List<ScriptLineVM> scriptLines)
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

                if (scriptLine.Status == "Rejected" && string.IsNullOrWhiteSpace(scriptLine.RejectionReason))
                {
                    errors.Add("Rejection reason is required for rejected medications.");
                }
            }

            return errors;
        }

        private async Task UpdateExistingScriptLine(ScriptLineVM scriptLineVM)
        {
            var existingScriptLine = await _context.ScriptLines
                .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineVM.ScriptLineId);

            if (existingScriptLine != null)
            {
                existingScriptLine.MedicationID = scriptLineVM.MedicationId;
                existingScriptLine.Quantity = scriptLineVM.Quantity;
                existingScriptLine.Instructions = scriptLineVM.Instructions ?? string.Empty;
                existingScriptLine.Repeats = scriptLineVM.IsRepeat ? 3 : 0;
                existingScriptLine.RepeatsLeft = scriptLineVM.RepeatsLeft;
                existingScriptLine.Status = scriptLineVM.Status ?? "Pending";
                existingScriptLine.RejectionReason = scriptLineVM.RejectionReason;

                UpdateScriptLineDates(existingScriptLine, scriptLineVM.Status);
            }
        }

        private async Task CreateNewScriptLine(ScriptLineVM scriptLineVM, int prescriptionId)
        {
            var newScriptLine = new ScriptLine
            {
                PrescriptionID = prescriptionId,
                MedicationID = scriptLineVM.MedicationId,
                Quantity = scriptLineVM.Quantity,
                Instructions = scriptLineVM.Instructions ?? string.Empty,
                Repeats = scriptLineVM.IsRepeat ? 3 : 0,
                RepeatsLeft = scriptLineVM.RepeatsLeft,
                Status = scriptLineVM.Status ?? "Pending",
                RejectionReason = scriptLineVM.RejectionReason
            };

            UpdateScriptLineDates(newScriptLine, scriptLineVM.Status);
            _context.ScriptLines.Add(newScriptLine);
        }

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

        private async Task UpdatePrescriptionStatus(CustomerScriptsVM model, Prescription prescription)
        {
            var approvedLines = model.ScriptLines.Count(sl => sl.Status == "Approved");
            var rejectedLines = model.ScriptLines.Count(sl => sl.Status == "Rejected");

            string oldStatus = prescription.Status;
            string newStatus;

            if (approvedLines > 0 && rejectedLines > 0)
                newStatus = "Partially Processed";
            else if (approvedLines > 0)
                newStatus = "Processed";
            else if (rejectedLines > 0)
                newStatus = "Rejected";
            else
                newStatus = "Pending";

            prescription.Status = newStatus;

            // Send email if status changed significantly
            if (oldStatus != newStatus && (newStatus == "Processed" || newStatus == "Partially Processed" || newStatus == "Rejected"))
            {
                // RELOAD the prescription with current script lines before sending email
                var updatedPrescription = await _context.Prescriptions
                    .Include(p => p.scriptLines)
                        .ThenInclude(sl => sl.Medications)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == prescription.PrescriptionID);

                if (updatedPrescription != null)
                {
                    SendPrescriptionStatusEmail(updatedPrescription, newStatus);
                }
            }
        }

        // Download prescription file
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id);

                if (prescription == null || prescription.Script == null)
                    return NotFound();

                return File(prescription.Script, "application/pdf", $"Prescription_{id}.pdf");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error downloading prescription: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // API: Check for allergy conflicts
        [HttpGet]
        public async Task<JsonResult> CheckAllergyConflicts(int prescriptionId, int medicationId)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == prescriptionId);

                if (prescription == null)
                    return Json(new { hasConflicts = false, conflicts = new string[0] });

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == prescription.ApplicationUserId);

                if (customer == null)
                    return Json(new { hasConflicts = false, conflicts = new string[0] });

                var customerAllergies = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == customer.CustormerID)
                    .Include(ca => ca.Active_Ingredient)
                    .Select(ca => ca.Active_Ingredient.Name)
                    .ToListAsync();

                var medicationIngredients = await _context.Medication_Ingredients
                    .Where(mi => mi.MedicationID == medicationId)
                    .Include(mi => mi.Active_Ingredients)
                    .Select(mi => mi.Active_Ingredients.Name)
                    .ToListAsync();

                var conflicts = customerAllergies
                    .Where(allergy => medicationIngredients.Any(ingredient =>
                        ingredient.Contains(allergy, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                return Json(new { hasConflicts = conflicts.Any(), conflicts });
            }
            catch (Exception)
            {
                return Json(new { hasConflicts = false, conflicts = new string[0] });
            }
        }

        // API: Get medication details for display
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

                string stockStatus = "Normal";
                if (medication.QuantityOnHand == 0)
                {
                    stockStatus = "OutOfStock";
                }
                else if (medication.QuantityOnHand <= 50 && medication.QuantityOnHand <= medication.ReOrderLevel)
                {
                    stockStatus = "VeryLowStock";
                }
                else if (medication.QuantityOnHand <= medication.ReOrderLevel)
                {
                    stockStatus = "LowStock";
                }

                return Json(new
                {
                    medicationName = medication.MedicationName,
                    activeIngredients = string.Join(", ", activeIngredients),
                    stock = medication.QuantityOnHand,
                    reorderLevel = medication.ReOrderLevel,
                    dosageForm = medication.DosageForm?.DosageFormName,
                    schedule = medication.Schedule,
                    price = medication.CurrentPrice,
                    stockStatus = stockStatus
                });
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Error loading medication: {ex.Message}" });
            }
        }

        // Helper method to reload ViewBags
        private async Task ReloadViewBags()
        {
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            ViewBag.Medications = await _context.Medications
                .Include(m => m.DosageForm)
                .Include(m => m.Medication_Ingredients)
                .ThenInclude(mi => mi.Active_Ingredients)
                .ToListAsync();
        }

        // GET: ProcessedScripts - Show all processed prescriptions
        public async Task<IActionResult> ProcessedScripts()
        {
            try
            {
                var processedPrescriptions = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Doctors)
                    .Include(p => p.scriptLines)
                    .Where(p => p.Status == "Processed" || p.Status == "Partially Processed" || p.Status == "Rejected")
                    .OrderByDescending(p => p.DateIssued)
                    .ToListAsync();

                return View(processedPrescriptions);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading processed prescriptions: {ex.Message}";
                return View(new List<Prescription>());
            }
        }

        // GET: ProcessedScripts Details - View details of a processed prescription
        public async Task<IActionResult> ProcessedDetails(int id)
        {
            try
            {
                var prescription = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .Include(p => p.Doctors)
                    .Include(p => p.scriptLines)
                        .ThenInclude(sl => sl.Medications)
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id);

                if (prescription == null)
                {
                    TempData["ErrorMessage"] = "Processed prescription not found";
                    return RedirectToAction(nameof(ProcessedScripts));
                }

                var viewModel = new ProcessedPrescriptionVM
                {
                    PrescriptionID = prescription.PrescriptionID,
                    PatientName = $"{prescription.ApplicationUser?.Name} {prescription.ApplicationUser?.Surname}",
                    IDNumber = prescription.ApplicationUser?.IDNumber ?? "",
                    Email = prescription.ApplicationUser?.Email ?? "",
                    DateIssued = prescription.DateIssued,
                    DoctorName = prescription.Doctors != null ?
                        $"{prescription.Doctors.Name} {prescription.Doctors.Surname}" : "Not Assigned",
                    Status = prescription.Status ?? "Unknown",
                    ScriptLines = prescription.scriptLines.Select(sl => new ProcessedScriptLineVM
                    {
                        MedicationName = sl.Medications?.MedicationName ?? "Unknown",
                        Quantity = sl.Quantity,
                        Instructions = sl.Instructions ?? "",
                        Status = sl.Status ?? "Pending",
                        RejectionReason = sl.RejectionReason,
                        ApprovedDate = sl.ApprovedDate,
                        RejectedDate = sl.RejectedDate
                    }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading prescription details: {ex.Message}";
                return RedirectToAction(nameof(ProcessedScripts));
            }
        }
    }
}