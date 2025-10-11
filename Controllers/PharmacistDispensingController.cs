using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Phamacist")]
    public class PharmacistDispensingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmacistDispensingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Main dispensing dashboard
        public async Task<IActionResult> Index()
        {
            try
            {
                var dashboard = new PharmacistDispensingDashboardVM
                {
                    PendingOrders = await _context.Orders
                        .Where(o => o.Status == "Ordered")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderBy(o => o.OrderDate)
                        .ToListAsync(),

                    ReadyForCollection = await _context.Orders
                        .Where(o => o.Status == "Ready for Collection")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderBy(o => o.OrderDate)
                        .ToListAsync(),

                    WaitingCustomerAction = await _context.Orders
                        .Where(o => o.Status == "Waiting Customer Action")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderBy(o => o.OrderDate)
                        .ToListAsync(),

                    TodayDispensed = await _context.Orders
                        .Where(o => o.Status == "Ready for Collection" &&
                                   o.OrderDate.Date == DateTime.Today)
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .CountAsync()
                };

                return View(dashboard);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading dashboard: {ex.Message}";
                return View(new PharmacistDispensingDashboardVM());
            }
        }

        // GET: Order details for dispensing
        public async Task<IActionResult> DispenseOrder(int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.ScriptLine)
                            .ThenInclude(sl => sl.Prescriptions)
                                .ThenInclude(p => p.Doctors)
                    .Include(o => o.Pharmacist)
                    .FirstOrDefaultAsync(o => o.OrderID == id);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Order not found";
                    return RedirectToAction(nameof(Index));
                }

                // Check customer allergies for all medications in order
                var customerAllergies = await _context.Custormer_Allergies
                    .Where(ca => ca.CustomerID == order.CustomerID)
                    .Include(ca => ca.Active_Ingredient)
                    .Select(ca => ca.Active_Ingredient.Name)
                    .ToListAsync();

                var viewModel = new DispenseOrderVM
                {
                    OrderID = order.OrderID,
                    OrderNumber = order.OrderNumber,
                    OrderDate = order.OrderDate,
                    CustomerName = $"{order.Customer.ApplicationUser.Name} {order.Customer.ApplicationUser.Surname}",
                    CustomerEmail = order.Customer.ApplicationUser.Email,
                    CustomerIDNumber = order.Customer.ApplicationUser.IDNumber,
                    CustomerAllergies = customerAllergies,
                    CurrentStatus = order.Status,
                    TotalAmount = decimal.Parse(order.TotalDue),
                    VAT = order.VAT,
                    OrderLines = order.OrderLines.Select(ol => new DispenseOrderLineVM
                    {
                        OrderLineID = ol.OrderLineID,
                        MedicationID = ol.MedicationID,
                        MedicationName = ol.Medications.MedicationName,
                        Quantity = ol.Quantity,
                        ItemPrice = ol.ItemPrice,
                        LineTotal = ol.ItemPrice * ol.Quantity,
                        Instructions = ol.ScriptLine?.Instructions ?? "Take as directed",
                        DoctorName = ol.ScriptLine?.Prescriptions?.Doctors != null ?
                            $"Dr. {ol.ScriptLine.Prescriptions.Doctors.Name} {ol.ScriptLine.Prescriptions.Doctors.Surname}" : "N/A",
                        Schedule = ol.Medications.Schedule,
                        CurrentStock = ol.Medications.QuantityOnHand,
                        IsLowStock = ol.Medications.QuantityOnHand <= ol.Medications.ReOrderLevel,
                        Status = ol.Status,
                        CanDispense = ol.Status == "Pending",
                        RejectionReason = ol.RejectionReason
                    }).ToList(),
                    AllItemsProcessed = order.OrderLines.All(ol =>
                        ol.Status == "Dispensed" || ol.Status == "Rejected"),
                    AnyItemsDispensed = order.OrderLines.Any(ol => ol.Status == "Dispensed"),
                    AllItemsRejected = order.OrderLines.All(ol => ol.Status == "Rejected")
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading order: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Dispense selected order lines
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DispenseSelectedOrderLines(int orderId, List<int> selectedOrderLineIds)
        {
            try
            {
                if (selectedOrderLineIds == null || !selectedOrderLineIds.Any())
                {
                    return Json(new { success = false, message = "Please select at least one medication to dispense." });
                }

                var order = await _context.Orders
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    return Json(new { success = false, message = "Order not found." });
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pharmacist = await _context.Pharmacists
                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

                if (pharmacist == null)
                {
                    return Json(new { success = false, message = "Pharmacist not found." });
                }

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var orderLinesToDispense = order.OrderLines
                        .Where(ol => selectedOrderLineIds.Contains(ol.OrderLineID) && ol.Status == "Pending")
                        .ToList();

                    // Check stock availability for all selected medications
                    var insufficientStockLines = orderLinesToDispense
                        .Where(ol => ol.Medications.QuantityOnHand < ol.Quantity)
                        .ToList();

                    if (insufficientStockLines.Any())
                    {
                        var medicationNames = insufficientStockLines
                            .Select(ol => ol.Medications.MedicationName)
                            .ToList();

                        return Json(new
                        {
                            success = false,
                            message = $"Insufficient stock for: {string.Join(", ", medicationNames)}"
                        });
                    }

                    // Dispense selected order lines
                    foreach (var orderLine in orderLinesToDispense)
                    {
                        // Update stock
                        orderLine.Medications.QuantityOnHand -= orderLine.Quantity;

                        // Update order line status
                        orderLine.Status = "Dispensed";
                    }

                    // Set dispensing pharmacist if not set
                    if (order.PharmacistID == null)
                    {
                        order.PharmacistID = pharmacist.PharmacistID;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Json(new
                    {
                        success = true,
                        message = $"{orderLinesToDispense.Count} medication(s) dispensed successfully!",
                        processedCount = orderLinesToDispense.Count
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, message = $"Error dispensing medications: {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // POST: Reject order line with reason
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectOrderLine(int orderLineId, string rejectionReason)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rejectionReason))
                {
                    return Json(new { success = false, message = "Rejection reason is required." });
                }

                var orderLine = await _context.OrderLines
                    .Include(ol => ol.Order)
                    .FirstOrDefaultAsync(ol => ol.OrderLineID == orderLineId);

                if (orderLine == null)
                {
                    return Json(new { success = false, message = "Order line not found." });
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pharmacist = await _context.Pharmacists
                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

                if (pharmacist == null)
                {
                    return Json(new { success = false, message = "Pharmacist not found." });
                }

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Update order line status and rejection reason
                    orderLine.Status = "Rejected";
                    orderLine.RejectionReason = rejectionReason.Trim();

                    // Set dispensing pharmacist if not set
                    if (orderLine.Order.PharmacistID == null)
                    {
                        orderLine.Order.PharmacistID = pharmacist.PharmacistID;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Json(new
                    {
                        success = true,
                        message = "Medication rejected successfully!",
                        newStatus = orderLine.Status
                    });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, message = $"Error rejecting medication: {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // POST: Complete order processing (Final "Complete Order Processing" button)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteOrderProcessing(int orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderLines)
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Order not found";
                    return RedirectToAction(nameof(Index));
                }

                // Check if all order lines have been processed
                var unprocessedLines = order.OrderLines.Where(ol => ol.Status == "Pending").ToList();
                if (unprocessedLines.Any())
                {
                    TempData["ErrorMessage"] = "Please process all medications before completing the order.";
                    return RedirectToAction(nameof(DispenseOrder), new { id = orderId });
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pharmacist = await _context.Pharmacists
                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

                if (pharmacist == null)
                {
                    TempData["ErrorMessage"] = "Pharmacist not found";
                    return RedirectToAction(nameof(DispenseOrder), new { id = orderId });
                }

                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Update order status based on processing outcomes
                    var dispensedLines = order.OrderLines.Count(ol => ol.Status == "Dispensed");
                    var rejectedLines = order.OrderLines.Count(ol => ol.Status == "Rejected");

                    if (dispensedLines > 0)
                    {
                        order.Status = "Ready for Collection";
                        TempData["SuccessMessage"] = $"Order {order.OrderNumber} is ready for collection!";
                    }
                    else if (rejectedLines == order.OrderLines.Count)
                    {
                        order.Status = "Waiting Customer Action";
                        TempData["WarningMessage"] = $"Order {order.OrderNumber} requires customer action. All medications were rejected.";
                    }

                    // Ensure pharmacist is set
                    if (order.PharmacistID == null)
                    {
                        order.PharmacistID = pharmacist.PharmacistID;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    TempData["ErrorMessage"] = $"Error completing order processing: {ex.Message}";
                    return RedirectToAction(nameof(DispenseOrder), new { id = orderId });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Order history and reporting
        public async Task<IActionResult> DispensingHistory(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var query = _context.Orders
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Where(o => o.Status == "Ready for Collection" || o.Status == "Waiting Customer Action");

                if (startDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate <= endDate.Value);
                }

                var orders = await query
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                var viewModel = new DispensingHistoryVM
                {
                    Orders = orders,
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalProcessed = orders.Count,
                    ReadyForCollectionCount = orders.Count(o => o.Status == "Ready for Collection"),
                    WaitingActionCount = orders.Count(o => o.Status == "Waiting Customer Action"),
                    TotalRevenue = orders.Where(o => o.Status == "Ready for Collection").Sum(o => decimal.Parse(o.TotalDue))
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading history: {ex.Message}";
                return View(new DispensingHistoryVM());
            }
        }

        // API: Get low stock medications
        [HttpGet]
        public async Task<JsonResult> GetLowStockMedications()
        {
            try
            {
                var lowStockMedications = await _context.Medications
                    .Where(m => m.QuantityOnHand <= m.ReOrderLevel)
                    .Select(m => new
                    {
                        m.MedcationID,
                        m.MedicationName,
                        m.QuantityOnHand,
                        m.ReOrderLevel,
                        Urgency = m.QuantityOnHand == 0 ? "Out of Stock" :
                                 m.QuantityOnHand <= 5 ? "Critical" : "Low"
                    })
                    .ToListAsync();

                return Json(new { success = true, medications = lowStockMedications });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}