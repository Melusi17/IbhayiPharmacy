using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using IbhayiPharmacy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Customer")]
    public class CustomerDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new Random();
        private readonly INotificationService _notificationService;

        public CustomerDashboardController(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // Main Dashboard View
        public IActionResult Index()
        {
            return View();
        }

        // UPDATED: Upload Prescription Section - GET with order tracking
        public async Task<IActionResult> UploadPrescription()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var unprocessedPrescriptions = await _context.Prescriptions
                .Where(p => p.ApplicationUserId == userId &&
                       (p.Status == null || p.Status == "Unprocessed" || p.Status == "Pending"))
                .OrderByDescending(p => p.DateIssued)
                .ToListAsync();

            // Get processed prescriptions
            var processedPrescriptions = await _context.Prescriptions
                .Where(p => p.ApplicationUserId == userId &&
                       (p.Status == "Processed" || p.Status == "Partially Processed"))
                .Include(p => p.Doctors)
                .Include(p => p.scriptLines)
                .OrderByDescending(p => p.DateIssued)
                .ToListAsync();

            // Get all order lines for the user's prescriptions
            var prescriptionIds = processedPrescriptions.Select(p => p.PrescriptionID).ToList();

            var orderLinesForPrescriptions = await _context.OrderLines
                .Where(ol => prescriptionIds.Contains(ol.ScriptLine.PrescriptionID) && ol.Status != "Cancelled")
                .Include(ol => ol.ScriptLine)
                .ToListAsync();

            // Group order lines by prescription ID
            var orderLinesByPrescription = orderLinesForPrescriptions
                .GroupBy(ol => ol.ScriptLine.PrescriptionID)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Calculate order status for each prescription
            foreach (var prescription in processedPrescriptions)
            {
                if (prescription.scriptLines != null && prescription.scriptLines.Any())
                {
                    var totalMedications = prescription.scriptLines.Count;

                    // Count how many script lines have been ordered
                    var orderedMedications = 0;
                    if (orderLinesByPrescription.TryGetValue(prescription.PrescriptionID, out var orderLines))
                    {
                        // Get unique script lines that have been ordered
                        var orderedScriptLineIds = orderLines.Select(ol => ol.ScriptLineID).Distinct();
                        orderedMedications = prescription.scriptLines
                            .Count(sl => orderedScriptLineIds.Contains(sl.ScriptLineID));
                    }

                    // Add dynamic properties
                    prescription.IsFullyOrdered = orderedMedications == totalMedications;
                    prescription.IsPartiallyOrdered = orderedMedications > 0 && orderedMedications < totalMedications;
                }
                else
                {
                    prescription.IsFullyOrdered = false;
                    prescription.IsPartiallyOrdered = false;
                }
            }

            var model = new CustomerDashboardVM
            {
                UnprocessedPrescriptions = unprocessedPrescriptions,
                ProcessedPrescriptions = processedPrescriptions
            };

            return View(model);
        }

        // Upload Prescription - POST (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UploadPrescription(IFormFile file, bool dispenseUponApproval)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, message = "Please select a file to upload." });
                }

                // Validate file type
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".pdf")
                {
                    return Json(new { success = false, message = "Only PDF files are allowed." });
                }

                // Validate file size (optional: limit to 10MB)
                if (file.Length > 10 * 1024 * 1024) // 10MB
                {
                    return Json(new { success = false, message = "File size must be less than 10MB." });
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Create new prescription
                var prescription = new Prescription
                {
                    ApplicationUserId = userId,
                    DateIssued = DateTime.Now,
                    DispenseUponApproval = dispenseUponApproval,
                    Status = "Unprocessed"
                };

                // Store file as byte array in database ONLY (no file system storage)
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    prescription.Script = ms.ToArray();
                }

                // Save to database
                _context.Prescriptions.Add(prescription);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Prescription uploaded successfully!",
                    prescriptionId = prescription.PrescriptionID,
                    date = prescription.DateIssued.ToString("yyyy-MM-dd"),
                    status = prescription.Status,
                    dispense = prescription.DispenseUponApproval ? "Yes" : "No"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error uploading prescription: {ex.Message}" });
            }
        }

        // Download Prescription
        public async Task<IActionResult> DownloadPrescription(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(p => p.PrescriptionID == id && p.ApplicationUserId == userId);

            if (prescription == null || prescription.Script == null)
                return NotFound();

            return File(prescription.Script, "application/pdf", $"Prescription_{id.ToString("D3")}.pdf");
        }

        // Place Orders Section
        public async Task<IActionResult> PlaceOrder()
        {
            var model = new PlaceOrderVM
            {
                Doctors = await _context.Doctors.ToListAsync(),
                Medications = await _context.Medications
                    .Include(m => m.DosageForm)
                    .Include(m => m.Medication_Ingredients)
                    .ThenInclude(mi => mi.Active_Ingredients)
                    .ToListAsync(),
                OrderDate = DateTime.Now
            };

            return View(model);
        }

        // Track Orders Section - SIMPLIFIED (Server-Side Rendering)
        public async Task<IActionResult> TrackOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Where(o => o.Customer.ApplicationUserId == userId)
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.Medications)
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.ScriptLine)
                        .ThenInclude(sl => sl.Prescriptions)
                            .ThenInclude(p => p.Doctors)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // Manage Repeats Section
        public async Task<IActionResult> ManageRepeats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var repeats = await _context.ScriptLines
                .Where(sl => sl.Prescriptions.ApplicationUserId == userId &&
                       sl.RepeatsLeft > 0)
                .Include(sl => sl.Medications)
                .Include(sl => sl.Prescriptions)
                .Select(sl => new RepeatMedicationVM
                {
                    ScriptLineId = sl.ScriptLineID,
                    MedicationName = sl.Medications.MedicationName,
                    RepeatsLeft = sl.RepeatsLeft,
                    LastRefillDate = sl.ApprovedDate ?? DateTime.Now.AddDays(-30),
                    Instructions = sl.Instructions
                })
                .ToListAsync();

            return View(repeats);
        }

        // View Reports Section
        public IActionResult ViewReports()
        {
            return View();
        }

        // API: Get medications for a specific prescription
        [HttpGet]
        public async Task<JsonResult> GetPrescriptionMedications(int prescriptionId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // First, verify the prescription exists and belongs to the user
                var prescriptionExists = await _context.Prescriptions
                    .AnyAsync(p => p.PrescriptionID == prescriptionId && p.ApplicationUserId == userId);

                if (!prescriptionExists)
                {
                    return Json(new { success = false, message = "Prescription not found or access denied" });
                }

                // Get script lines with medications - FIXED: Proper include and handling
                var medications = await _context.ScriptLines
                    .Where(sl => sl.PrescriptionID == prescriptionId)
                    .Include(sl => sl.Medications)
                    .Select(sl => new
                    {
                        scriptLineId = sl.ScriptLineID,
                        medicationId = sl.MedicationID,
                        name = sl.Medications.MedicationName ?? "Unknown Medication",
                        instructions = sl.Instructions ?? "Take as directed",
                        repeats = sl.Repeats,
                        price = sl.Medications.CurrentPrice,
                        status = sl.Status ?? "Pending",
                        rejectionReason = sl.RejectionReason ?? "",
                        quantity = sl.Quantity
                    })
                    .ToListAsync();

                return Json(medications);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // API: Get medication details
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
            catch (Exception)
            {
                return Json(new { error = "Error loading medication details" });
            }
        }

        // API: Check for allergy conflicts
        [HttpGet]
        public async Task<JsonResult> CheckAllergyConflicts(int medicationId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

                if (customer == null)
                    return Json(new { hasConflicts = false, conflicts = new string[0] });

                var customerAllergies = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == customer.CustormerID)
                    .Include(ca => ca.Active_Ingredient)
                    .Select(ca => ca.Active_Ingredient!.Name)
                    .ToListAsync();

                var medicationIngredients = await _context.Medication_Ingredients
                    .Where(mi => mi.MedicationID == medicationId)
                    .Include(mi => mi.Active_Ingredients)
                    .Select(mi => mi.Active_Ingredients!.Name)
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

        // API: Submit order - UPDATED WITH ORDER NUMBER GENERATION
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SubmitOrder([FromBody] OrderSubmissionVM orderData)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

                if (customer == null)
                    return Json(new { success = false, message = "Customer not found" });

                // Generate unique order number
                string orderNumber = await GenerateUniqueOrderNumber();

                // Create new order with generated order number
                var order = new Order
                {
                    CustomerID = customer.CustormerID,
                    OrderDate = DateTime.Now,
                    Status = "Ordered",
                    VAT = 15, // 15% VAT
                    OrderNumber = orderNumber
                };

                // Calculate total
                decimal subtotal = 0;
                foreach (var item in orderData.OrderItems)
                {
                    var medication = await _context.Medications
                        .FirstOrDefaultAsync(m => m.MedcationID == item.MedicationId);

                    if (medication != null)
                    {
                        subtotal += medication.CurrentPrice * item.Quantity;

                        var orderLine = new OrderLine
                        {
                            MedicationID = item.MedicationId,
                            Quantity = item.Quantity,
                            ItemPrice = medication.CurrentPrice,
                            ScriptLineID = item.ScriptLineId,
                            Status = "Pending"
                        };

                        order.OrderLines.Add(orderLine);
                    }
                }

                order.TotalDue = (subtotal + (subtotal * order.VAT / 100)).ToString("F2");

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Order submitted successfully!",
                    orderId = order.OrderID,
                    orderNumber = order.OrderNumber
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error submitting order: {ex.Message}" });
            }
        }

        // Generate unique order number - UPDATED SHORTER FORMAT
        private async Task<string> GenerateUniqueOrderNumber()
        {
            string orderNumber;
            bool isUnique;
            int maxAttempts = 5;
            int attempts = 0;

            do
            {
                // Format: ORD-YYYYMMDD-RRRR (RRRR = 4 random digits)
                string datePart = DateTime.Now.ToString("yyyyMMdd");
                string randomPart = _random.Next(1000, 10000).ToString(); // 4-digit random number
                orderNumber = $"ORD-{datePart}-{randomPart}";

                // Check if this order number already exists
                isUnique = !await _context.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
                attempts++;

            } while (!isUnique && attempts < maxAttempts);

            // Fallback if we still don't have a unique number
            if (!isUnique)
            {
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                orderNumber = $"ORD-{timestamp}";
            }

            return orderNumber;
        }

        // UPDATED: API: Request refill with anti-forgery token
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RequestRefill(int scriptLineId)
        {
            try
            {
                var scriptLine = await _context.ScriptLines
                    .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId);

                if (scriptLine == null)
                    return Json(new { success = false, message = "Prescription line not found" });

                if (scriptLine.RepeatsLeft <= 0)
                    return Json(new { success = false, message = "No repeats left for this medication" });

                scriptLine.RepeatsLeft--;
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Refill requested successfully!",
                    repeatsLeft = scriptLine.RepeatsLeft
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error requesting refill: {ex.Message}" });
            }
        }

        // API: Generate PDF Report
        [HttpPost]
        public async Task<IActionResult> GenerateReport([FromBody] ReportRequestVM request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var reportData = await _context.Orders
                    .Where(o => o.Customer.ApplicationUserId == userId &&
                           o.OrderDate.Date == request.ReportDate.Date)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Select(o => new
                    {
                        OrderDate = o.OrderDate,
                        Medications = o.OrderLines.Select(ol => new
                        {
                            Name = ol.Medications.MedicationName,
                            Quantity = ol.Quantity,
                            Price = ol.ItemPrice,
                            Status = ol.Status
                        }),
                        Total = o.TotalDue
                    })
                    .ToListAsync();

                return Json(new { success = true, data = reportData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error generating report: {ex.Message}" });
            }
        }

        // Profile Management
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ApplicationUser model)
        {
            if (!ModelState.IsValid)
                return View("Profile", model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.CellphoneNumber = model.CellphoneNumber;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        // API: Get prescription details for editing - FIXED ROUTE
        [HttpGet]
        [Route("CustomerDashboard/GetPrescriptionDetails")]
        public async Task<JsonResult> GetPrescriptionDetails(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var prescription = await _context.Prescriptions
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id && p.ApplicationUserId == userId);

                if (prescription == null)
                {
                    return Json(new { success = false, message = "Prescription not found" });
                }

                return Json(new
                {
                    success = true,
                    prescriptionId = prescription.PrescriptionID,
                    dateIssued = prescription.DateIssued.ToString("yyyy-MM-dd"),
                    status = prescription.Status ?? "Unprocessed",
                    dispenseUponApproval = prescription.DispenseUponApproval,
                    fileName = $"Prescription_{prescription.PrescriptionID.ToString("D3")}.pdf"
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error loading prescription details: {ex.Message}");
                return Json(new { success = false, message = $"Error loading prescription: {ex.Message}" });
            }
        }

        // API: Update prescription document - FIXED ROUTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CustomerDashboard/UpdatePrescriptionDocument")]
        public async Task<JsonResult> UpdatePrescriptionDocument(int id, IFormFile file, bool dispenseUponApproval)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var prescription = await _context.Prescriptions
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id && p.ApplicationUserId == userId);

                if (prescription == null)
                {
                    return Json(new { success = false, message = "Prescription not found" });
                }

                if (file != null && file.Length > 0)
                {
                    // Validate file type
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (fileExtension != ".pdf")
                    {
                        return Json(new { success = false, message = "Only PDF files are allowed." });
                    }

                    // Validate file size (optional: limit to 10MB)
                    if (file.Length > 10 * 1024 * 1024) // 10MB
                    {
                        return Json(new { success = false, message = "File size must be less than 10MB." });
                    }

                    // Update file
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        prescription.Script = ms.ToArray();
                    }
                }

                // Update other properties
                prescription.DispenseUponApproval = dispenseUponApproval;
                prescription.DateIssued = DateTime.Now; // Update timestamp when modified

                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Prescription updated successfully!",
                    date = prescription.DateIssued.ToString("yyyy-MM-dd"),
                    dispense = prescription.DispenseUponApproval ? "Yes" : "No"
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating prescription: {ex.Message}");
                return Json(new { success = false, message = $"Error updating prescription: {ex.Message}" });
            }
        }

        // API: Delete prescription - FIXED ROUTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CustomerDashboard/DeletePrescription")]
        public async Task<JsonResult> DeletePrescription(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var prescription = await _context.Prescriptions
                    .FirstOrDefaultAsync(p => p.PrescriptionID == id && p.ApplicationUserId == userId);

                if (prescription == null)
                {
                    return Json(new { success = false, message = "Prescription not found" });
                }

                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Prescription deleted successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting prescription: {ex.Message}");
                return Json(new { success = false, message = $"Error deleting prescription: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return PartialView("_NotificationsPartial", new CustomerNotificationViewModel());
                }

                // You might need to inject INotificationService here
                var notificationService = HttpContext.RequestServices.GetService<INotificationService>();
                var notifications = await notificationService.GetCustomerNotificationsAsync(userId);

                return PartialView("_NotificationsPartial", notifications);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error in GetNotifications: {ex.Message}");
                return PartialView("_NotificationsPartial", new CustomerNotificationViewModel());
            }
        }
    }

   
}