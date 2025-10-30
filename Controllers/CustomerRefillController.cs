//using IbhayiPharmacy.Data;
//using IbhayiPharmacy.Models;
//using IbhayiPharmacy.Models.PharmacistVM;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace IbhayiPharmacy.Controllers
//{
//    [Authorize(Policy = "Customer")]
//    public class CustomerRefillController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly Random _random = new Random();

//        public CustomerRefillController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Manage refills - shows medications with repeats available GROUPED BY PRESCRIPTION
//        public async Task<IActionResult> ManageRepeats()
//        {
//            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//            var refillScriptLines = await _context.ScriptLines
//                .Include(sl => sl.Medications)
//                .Include(sl => sl.Prescriptions)
//                    .ThenInclude(p => p.Doctors)
//                .Where(sl => sl.Prescriptions.ApplicationUserId == currentUserId &&
//                            sl.Status == "Approved" &&
//                            sl.RepeatsLeft > 0)
//                .Select(sl => new RefillMedicationVM
//                {
//                    ScriptLineID = sl.ScriptLineID,
//                    PrescriptionID = sl.PrescriptionID,
//                    MedicationID = sl.MedicationID,
//                    MedicationName = sl.Medications.MedicationName,
//                    Quantity = sl.Quantity,
//                    Instructions = sl.Instructions ?? string.Empty,
//                    TotalRepeats = sl.Repeats,
//                    RepeatsLeft = sl.RepeatsLeft,
//                    DoctorName = $"{sl.Prescriptions.Doctors.Name} {sl.Prescriptions.Doctors.Surname}",
//                    LastRefillDate = sl.ApprovedDate ?? sl.Prescriptions.DateIssued,
//                    CurrentPrice = sl.Medications.CurrentPrice,
//                    Schedule = sl.Medications.Schedule,
//                    PrescriptionDate = sl.Prescriptions.DateIssued
//                })
//                .ToListAsync();

//            // Group by prescription for the view
//            var prescriptionsGrouped = refillScriptLines
//                .GroupBy(x => new { x.PrescriptionID, x.DoctorName, x.PrescriptionDate })
//                .Select(g => new PrescriptionRefillVM
//                {
//                    PrescriptionID = g.Key.PrescriptionID,
//                    DoctorName = g.Key.DoctorName,
//                    PrescriptionDate = g.Key.PrescriptionDate,
//                    Medications = g.ToList()
//                })
//                .OrderByDescending(x => x.PrescriptionDate)
//                .ToList();

//            return View(prescriptionsGrouped);
//        }

//        // GET: Refill history for a specific medication
//        [HttpGet]
//        public async Task<JsonResult> GetRefillHistory(int scriptLineId)
//        {
//            try
//            {
//                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                // Get current script line
//                var currentScriptLine = await _context.ScriptLines
//                    .Include(sl => sl.Medications)
//                    .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId);

//                if (currentScriptLine == null)
//                {
//                    return Json(new { success = false, message = "Medication not found" });
//                }

//                // Get dispensed order lines for this script line
//                var dispensedOrders = await _context.OrderLines
//                    .Include(ol => ol.Order)
//                    .Where(ol => ol.ScriptLineID == scriptLineId && ol.Status == "Dispensed")
//                    .OrderBy(ol => ol.Order.OrderDate)
//                    .ToListAsync();

//                var dispensingHistory = new List<object>();
//                var totalRepeats = currentScriptLine.Repeats + dispensedOrders.Count;

//                // Add historical dispensing with correct repeats left
//                for (int i = 0; i < dispensedOrders.Count; i++)
//                {
//                    var orderLine = dispensedOrders[i];
//                    int repeatsLeftAfter = totalRepeats - (i + 1);

//                    dispensingHistory.Add(new
//                    {
//                        date = orderLine.Order.OrderDate.ToString("yyyy-MM-dd"),
//                        orderNumber = orderLine.Order.OrderNumber ?? "N/A",
//                        repeatsLeft = repeatsLeftAfter,
//                        status = "Dispensed"
//                    });
//                }

//                // Add current available state
//                dispensingHistory.Add(new
//                {
//                    date = DateTime.Now.ToString("yyyy-MM-dd"),
//                    orderNumber = "Available for Refill",
//                    repeatsLeft = currentScriptLine.RepeatsLeft,
//                    status = "Available"
//                });

//                return Json(new
//                {
//                    success = true,
//                    history = dispensingHistory,
//                    currentRepeats = currentScriptLine.RepeatsLeft,
//                    medicationName = currentScriptLine.Medications.MedicationName
//                });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = ex.Message });
//            }
//        }

//        // POST: Request refill - creates new order
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<JsonResult> RequestRefill(int scriptLineId)
//        {
//            try
//            {
//                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                // Validate script line exists and has repeats
//                var scriptLine = await _context.ScriptLines
//                    .Include(sl => sl.Medications)
//                    .Include(sl => sl.Prescriptions)
//                    .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId &&
//                                              sl.Prescriptions.ApplicationUserId == currentUserId &&
//                                              sl.Status == "Approved" &&
//                                              sl.RepeatsLeft > 0);

//                if (scriptLine == null)
//                {
//                    return Json(new { success = false, message = "Refill not available or medication not found." });
//                }

//                // Get customer
//                var customer = await _context.Customers
//                    .FirstOrDefaultAsync(c => c.ApplicationUserId == currentUserId);

//                if (customer == null)
//                {
//                    return Json(new { success = false, message = "Customer profile not found." });
//                }

//                using var transaction = await _context.Database.BeginTransactionAsync();

//                try
//                {
//                    // Generate unique refill order number
//                    string orderNumber = await GenerateRefillOrderNumber();

//                    // Create order
//                    var order = new Order
//                    {
//                        CustomerID = customer.CustormerID,
//                        OrderDate = DateTime.Now,
//                        Status = "Ordered",
//                        VAT = 15,
//                        OrderNumber = orderNumber
//                    };

//                    _context.Orders.Add(order);
//                    await _context.SaveChangesAsync();

//                    // Decrement repeats
//                    scriptLine.RepeatsLeft--;

//                    // Create order line
//                    var orderLine = new OrderLine
//                    {
//                        OrderID = order.OrderID,
//                        ScriptLineID = scriptLine.ScriptLineID,
//                        MedicationID = scriptLine.MedicationID,
//                        Quantity = scriptLine.Quantity,
//                        ItemPrice = scriptLine.Medications.CurrentPrice,
//                        Status = "Pending"
//                    };

//                    _context.OrderLines.Add(orderLine);

//                    // Calculate total
//                    decimal subtotal = scriptLine.Medications.CurrentPrice * scriptLine.Quantity;
//                    order.TotalDue = (subtotal + (subtotal * order.VAT / 100)).ToString("F2");

//                    await _context.SaveChangesAsync();
//                    await transaction.CommitAsync();

//                    return Json(new
//                    {
//                        success = true,
//                        message = "Refill requested successfully!",
//                        orderNumber = orderNumber,
//                        repeatsLeft = scriptLine.RepeatsLeft,
//                        scriptLineId = scriptLineId
//                    });
//                }
//                catch (Exception ex)
//                {
//                    await transaction.RollbackAsync();
//                    return Json(new { success = false, message = $"Error processing refill: {ex.Message}" });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = $"Error: {ex.Message}" });
//            }
//        }

//        // Generate unique refill order number
//        private async Task<string> GenerateRefillOrderNumber()
//        {
//            string orderNumber;
//            bool isUnique;
//            int maxAttempts = 5;
//            int attempts = 0;

//            do
//            {
//                string randomPart = _random.Next(1000, 10000).ToString();
//                orderNumber = $"ORD-Refill-{randomPart}";

//                isUnique = !await _context.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
//                attempts++;

//            } while (!isUnique && attempts < maxAttempts);

//            if (!isUnique)
//            {
//                string timestamp = DateTime.Now.ToString("HHmmss");
//                orderNumber = $"ORD-Refill-{timestamp}";
//            }

//            return orderNumber;
//        }

//        // GET: Order summary for display in modal
//        [HttpGet]
//        public async Task<JsonResult> GetOrderSummary(string orderNumber)
//        {
//            try
//            {
//                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                var order = await _context.Orders
//                    .Include(o => o.Customer)
//                        .ThenInclude(c => c.ApplicationUser)
//                    .Include(o => o.OrderLines)
//                        .ThenInclude(ol => ol.Medications)
//                    .Include(o => o.OrderLines)
//                        .ThenInclude(ol => ol.ScriptLine)
//                            .ThenInclude(sl => sl.Prescriptions)
//                                .ThenInclude(p => p.Doctors)
//                    .Where(o => o.OrderNumber == orderNumber &&
//                               o.Customer.ApplicationUserId == currentUserId)
//                    .Select(o => new
//                    {
//                        OrderNumber = o.OrderNumber,
//                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
//                        DoctorName = o.OrderLines.First().ScriptLine.Prescriptions.Doctors != null ?
//                            $"Dr. {o.OrderLines.First().ScriptLine.Prescriptions.Doctors.Name} {o.OrderLines.First().ScriptLine.Prescriptions.Doctors.Surname}" : "N/A",
//                        Medications = o.OrderLines.Select(ol => new
//                        {
//                            Name = ol.Medications.MedicationName,
//                            Quantity = ol.Quantity,
//                            Price = ol.ItemPrice,
//                            Subtotal = ol.ItemPrice * ol.Quantity
//                        }),
//                        Total = o.TotalDue
//                    })
//                    .FirstOrDefaultAsync();

//                if (order == null)
//                {
//                    return Json(new { success = false, message = "Order not found." });
//                }

//                return Json(new { success = true, order = order });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = ex.Message });
//            }
//        }
//    }
//}

using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Customer")]
    public class CustomerRefillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new Random();

        public CustomerRefillController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manage refills - shows medications with repeats available GROUPED BY PRESCRIPTION
        public async Task<IActionResult> ManageRepeats()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var refillScriptLines = await _context.ScriptLines
                .Include(sl => sl.Medications)
                .Include(sl => sl.Prescriptions)
                    .ThenInclude(p => p.Doctors)
                .Where(sl => sl.Prescriptions.ApplicationUserId == currentUserId &&
                            sl.Status == "Approved" &&
                            sl.RepeatsLeft > 0)
                .Select(sl => new RefillMedicationVM
                {
                    ScriptLineID = sl.ScriptLineID,
                    PrescriptionID = sl.PrescriptionID,
                    MedicationID = sl.MedicationID,
                    MedicationName = sl.Medications.MedicationName,
                    Quantity = sl.Quantity,
                    Instructions = sl.Instructions ?? string.Empty,
                    TotalRepeats = sl.Repeats,
                    RepeatsLeft = sl.RepeatsLeft,
                    DoctorName = $"{sl.Prescriptions.Doctors.Name} {sl.Prescriptions.Doctors.Surname}",
                    LastRefillDate = sl.ApprovedDate ?? sl.Prescriptions.DateIssued,
                    CurrentPrice = sl.Medications.CurrentPrice,
                    Schedule = sl.Medications.Schedule,
                    PrescriptionDate = sl.Prescriptions.DateIssued
                })
                .ToListAsync();

            // Group by prescription for the view
            var prescriptionsGrouped = refillScriptLines
                .GroupBy(x => new { x.PrescriptionID, x.DoctorName, x.PrescriptionDate })
                .Select(g => new PrescriptionRefillVM
                {
                    PrescriptionID = g.Key.PrescriptionID,
                    DoctorName = g.Key.DoctorName,
                    PrescriptionDate = g.Key.PrescriptionDate,
                    Medications = g.ToList()
                })
                .OrderByDescending(x => x.PrescriptionDate)
                .ToList();

            return View(prescriptionsGrouped);
        }

        // GET: Refill history for a specific medication
        [HttpGet]
        public async Task<IActionResult> GetRefillHistory(int scriptLineId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Get current script line
                var currentScriptLine = await _context.ScriptLines
                    .Include(sl => sl.Medications)
                    .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId);

                if (currentScriptLine == null)
                {
                    return Json(new { success = false, message = "Medication not found" });
                }

                // Verify the script line belongs to the current user
                var userScriptLine = await _context.ScriptLines
                    .Include(sl => sl.Prescriptions)
                    .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId &&
                                              sl.Prescriptions.ApplicationUserId == currentUserId);

                if (userScriptLine == null)
                {
                    return Json(new { success = false, message = "Access denied" });
                }

                // Get dispensed order lines for this script line
                var dispensedOrders = await _context.OrderLines
                    .Include(ol => ol.Order)
                    .Where(ol => ol.ScriptLineID == scriptLineId && ol.Status == "Dispensed")
                    .OrderBy(ol => ol.Order.OrderDate)
                    .ToListAsync();

                var dispensingHistory = new List<object>();
                var totalRepeats = currentScriptLine.Repeats + dispensedOrders.Count;

                // Add historical dispensing with correct repeats left
                for (int i = 0; i < dispensedOrders.Count; i++)
                {
                    var orderLine = dispensedOrders[i];
                    int repeatsLeftAfter = totalRepeats - (i + 1);

                    dispensingHistory.Add(new
                    {
                        date = orderLine.Order.OrderDate.ToString("yyyy-MM-dd"),
                        orderNumber = orderLine.Order.OrderNumber ?? "N/A",
                        repeatsLeft = repeatsLeftAfter,
                        status = "Dispensed"
                    });
                }

                // Add current available state
                dispensingHistory.Add(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd"),
                    orderNumber = "Available for Refill",
                    repeatsLeft = currentScriptLine.RepeatsLeft,
                    status = "Available"
                });

                return Json(new
                {
                    success = true,
                    history = dispensingHistory,
                    currentRepeats = currentScriptLine.RepeatsLeft,
                    medicationName = currentScriptLine.Medications.MedicationName
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while loading refill history" });
            }
        }

        // POST: Request refill - creates new order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestRefill(int scriptLineId)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Console.WriteLine($"=== REFILL REQUEST START ===");
                Console.WriteLine($"User: {currentUserId}, ScriptLine: {scriptLineId}");

                // Use the same approach as the debug endpoint - find first, then validate
                var scriptLine = await _context.ScriptLines
                    .Include(sl => sl.Medications)
                    .Include(sl => sl.Prescriptions)
                    .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId);

                Console.WriteLine($"ScriptLine found: {scriptLine != null}");

                if (scriptLine == null)
                {
                    Console.WriteLine("ScriptLine is null");
                    return Json(new { success = false, message = "Medication not found in system." });
                }

                Console.WriteLine($"ScriptLine details - Status: {scriptLine.Status}, RepeatsLeft: {scriptLine.RepeatsLeft}, PrescriptionUser: {scriptLine.Prescriptions?.ApplicationUserId}");

                // Validate conditions separately
                if (scriptLine.Prescriptions?.ApplicationUserId != currentUserId)
                {
                    Console.WriteLine($"User mismatch: {scriptLine.Prescriptions?.ApplicationUserId} vs {currentUserId}");
                    return Json(new
                    {
                        success = false,
                        message = "This medication doesn't belong to your account."
                    });
                }

                if (scriptLine.Status != "Approved")
                {
                    Console.WriteLine($"Status not approved: {scriptLine.Status}");
                    return Json(new
                    {
                        success = false,
                        message = $"This medication is not approved for refill. Current status: {scriptLine.Status}"
                    });
                }

                if (scriptLine.RepeatsLeft <= 0)
                {
                    Console.WriteLine($"No repeats left: {scriptLine.RepeatsLeft}");
                    return Json(new
                    {
                        success = false,
                        message = "No repeats left for this medication."
                    });
                }

                Console.WriteLine("All validation passed - proceeding with refill");

                // Get customer
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == currentUserId);

                if (customer == null)
                {
                    Console.WriteLine("Customer not found");
                    return Json(new { success = false, message = "Customer profile not found." });
                }

                Console.WriteLine($"Customer found: {customer.CustormerID}");

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Generate unique refill order number
                    string orderNumber = await GenerateRefillOrderNumber();
                    Console.WriteLine($"Order number: {orderNumber}");

                    // Create order
                    var order = new Order
                    {
                        CustomerID = customer.CustormerID,
                        OrderDate = DateTime.Now,
                        Status = "Ordered",
                        VAT = 15,
                        OrderNumber = orderNumber
                    };

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    Console.WriteLine($"Order created: {order.OrderID}");

                    // Decrement repeats
                    scriptLine.RepeatsLeft--;
                    Console.WriteLine($"Repeats left after decrement: {scriptLine.RepeatsLeft}");

                    // Create order line
                    var orderLine = new OrderLine
                    {
                        OrderID = order.OrderID,
                        ScriptLineID = scriptLine.ScriptLineID,
                        MedicationID = scriptLine.MedicationID,
                        Quantity = scriptLine.Quantity,
                        ItemPrice = scriptLine.Medications.CurrentPrice,
                        Status = "Pending"
                    };

                    _context.OrderLines.Add(orderLine);

                    // Calculate total
                    decimal subtotal = scriptLine.Medications.CurrentPrice * scriptLine.Quantity;
                    order.TotalDue = (subtotal + (subtotal * order.VAT / 100)).ToString("F2");

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    Console.WriteLine($"=== REFILL SUCCESSFUL ===");

                    return Json(new
                    {
                        success = true,
                        message = "Refill requested successfully!",
                        orderNumber = orderNumber,
                        repeatsLeft = scriptLine.RepeatsLeft,
                        scriptLineId = scriptLineId
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Transaction error: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    return Json(new { success = false, message = $"Error processing refill: {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // Generate unique refill order number
        private async Task<string> GenerateRefillOrderNumber()
        {
            string orderNumber;
            bool isUnique;
            int maxAttempts = 5;
            int attempts = 0;

            do
            {
                string randomPart = _random.Next(1000, 10000).ToString();
                orderNumber = $"ORD-Refill-{randomPart}";

                isUnique = !await _context.Orders.AnyAsync(o => o.OrderNumber == orderNumber);
                attempts++;

            } while (!isUnique && attempts < maxAttempts);

            if (!isUnique)
            {
                string timestamp = DateTime.Now.ToString("HHmmss");
                orderNumber = $"ORD-Refill-{timestamp}";
            }

            return orderNumber;
        }

        // GET: Order summary for display in modal
        [HttpGet]
        public async Task<IActionResult> GetOrderSummary(string orderNumber)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var order = await _context.Orders
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.ScriptLine)
                            .ThenInclude(sl => sl.Prescriptions)
                                .ThenInclude(p => p.Doctors)
                    .Where(o => o.OrderNumber == orderNumber &&
                               o.Customer.ApplicationUserId == currentUserId)
                    .Select(o => new
                    {
                        OrderNumber = o.OrderNumber,
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                        DoctorName = o.OrderLines.First().ScriptLine.Prescriptions.Doctors != null ?
                            $"Dr. {o.OrderLines.First().ScriptLine.Prescriptions.Doctors.Name} {o.OrderLines.First().ScriptLine.Prescriptions.Doctors.Surname}" : "N/A",
                        Medications = o.OrderLines.Select(ol => new
                        {
                            Name = ol.Medications.MedicationName,
                            Quantity = ol.Quantity,
                            Price = ol.ItemPrice,
                            Subtotal = ol.ItemPrice * ol.Quantity
                        }),
                        Total = o.TotalDue
                    })
                    .FirstOrDefaultAsync();

                if (order == null)
                {
                    return Json(new { success = false, message = "Order not found." });
                }

                return Json(new { success = true, order = order });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while loading order summary" });
            }
        }

        // ADD THESE DIAGNOSTIC ENDPOINTS TO YOUR CONTROLLER:

        // Diagnostic endpoint to check script line details
        [HttpGet]
        public async Task<IActionResult> DebugScriptLine(int scriptLineId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var scriptLine = await _context.ScriptLines
                .Include(sl => sl.Medications)
                .Include(sl => sl.Prescriptions)
                .FirstOrDefaultAsync(sl => sl.ScriptLineID == scriptLineId);

            if (scriptLine == null)
            {
                return Json(new
                {
                    found = false,
                    message = "ScriptLine not found in database"
                });
            }

            return Json(new
            {
                found = true,
                scriptLineId = scriptLine.ScriptLineID,
                medicationId = scriptLine.MedicationID,
                medicationName = scriptLine.Medications?.MedicationName,
                status = scriptLine.Status,
                repeatsLeft = scriptLine.RepeatsLeft,
                prescriptionUserId = scriptLine.Prescriptions?.ApplicationUserId,
                currentUserId = currentUserId,
                userMatches = scriptLine.Prescriptions?.ApplicationUserId == currentUserId,
                isApproved = scriptLine.Status == "Approved",
                hasRepeats = scriptLine.RepeatsLeft > 0,
                meetsAllConditions = scriptLine.Prescriptions?.ApplicationUserId == currentUserId &&
                                   scriptLine.Status == "Approved" &&
                                   scriptLine.RepeatsLeft > 0
            });
        }



        // Diagnostic endpoint to list all available script lines for current user
        [HttpGet]
        public async Task<IActionResult> DebugAvailableRefills()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var availableScriptLines = await _context.ScriptLines
                .Include(sl => sl.Medications)
                .Include(sl => sl.Prescriptions)
                .Where(sl => sl.Prescriptions.ApplicationUserId == currentUserId)
                .Select(sl => new
                {
                    ScriptLineID = sl.ScriptLineID,
                    MedicationName = sl.Medications.MedicationName,
                    Status = sl.Status,
                    RepeatsLeft = sl.RepeatsLeft,
                    PrescriptionUserId = sl.Prescriptions.ApplicationUserId,
                    MeetsRefillConditions = sl.Status == "Approved" && sl.RepeatsLeft > 0
                })
                .ToListAsync();

            return Json(availableScriptLines);
        }
    }
}