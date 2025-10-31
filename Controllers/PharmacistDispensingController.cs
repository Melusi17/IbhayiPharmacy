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
    public class PharmacistDispensingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _email;

        public PharmacistDispensingController(ApplicationDbContext context, EmailService email)
        {
            _context = context;
            _email = email;
        }

        // GET: Main dispensing dashboard - ONLY active orders
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

        // GET: Collection Tracking - ONLY ready/collected orders
        public async Task<IActionResult> CollectionTracking()
        {
            try
            {
                var collectionOrders = await _context.Orders
                    .Where(o => o.Status == "Ready for Collection" || o.Status == "Collected")
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.ScriptLine)
                            .ThenInclude(sl => sl.Prescriptions)
                                .ThenInclude(p => p.Doctors)
                    .Include(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                var viewModel = new CollectionTrackingVM
                {
                    Orders = collectionOrders,
                    ReadyForCollectionCount = collectionOrders.Count(o => o.Status == "Ready for Collection"),
                    CollectedCount = collectionOrders.Count(o => o.Status == "Collected"),
                    TotalOrders = collectionOrders.Count
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading collection orders: {ex.Message}";
                return View(new CollectionTrackingVM());
            }
        }

        // POST: Mark order as collected - FIXED VERSION
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsCollected([FromBody] MarkAsCollectedRequest request)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.ScriptLine)
                    .FirstOrDefaultAsync(o => o.OrderID == request.OrderId);

                if (order == null)
                {
                    return Json(new { success = false, message = $"Order with ID {request.OrderId} not found in database." });
                }

                if (order.Status != "Ready for Collection")
                {
                    return Json(new { success = false, message = $"Order status is '{order.Status}', not 'Ready for Collection'." });
                }

                order.Status = "Collected";
                await _context.SaveChangesAsync();

                // ADDED: Send collection confirmation email
                SendCollectionConfirmationEmail(order);

                return Json(new { success = true, message = "Order marked as collected successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error marking order as collected: {ex.Message}" });
            }
        }

        public class MarkAsCollectedRequest
        {
            public int OrderId { get; set; }
        }

        // POST: Send collection email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCollectionEmail([FromBody] SendEmailRequest request)
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
                    .Include(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(o => o.OrderID == request.OrderId);

                if (order == null)
                {
                    return Json(new { success = false, message = "Order not found." });
                }

                var customerEmail = order.Customer.ApplicationUser.Email;
                var customerName = $"{order.Customer.ApplicationUser.Name} {order.Customer.ApplicationUser.Surname}";

                // Send actual email using our email service
                SendOrderReadyEmail(order);

                return Json(new
                {
                    success = true,
                    message = $"Collection notification sent to {customerName} at {customerEmail}"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending email: {ex.Message}" });
            }
        }

        public class SendEmailRequest
        {
            public int OrderId { get; set; }
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
                        RejectionReason = ol.RejectionReason,
                        TotalRepeats = ol.ScriptLine?.Repeats ?? 0,
                        RepeatsLeft = ol.ScriptLine?.RepeatsLeft ?? 0
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
                return RedirectToAction(nameof(CollectionTracking));
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
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.ScriptLine)
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .Include(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
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
                    var orderLinesToProcess = order.OrderLines
                        .Where(ol => selectedOrderLineIds.Contains(ol.OrderLineID) && ol.Status == "Pending")
                        .ToList();

                    var orderLinesToDispense = new List<OrderLine>();
                    var orderLinesToReject = new List<OrderLine>();

                    foreach (var orderLine in orderLinesToProcess)
                    {
                        if (orderLine.ScriptLine != null && orderLine.ScriptLine.Repeats > 0 && orderLine.ScriptLine.RepeatsLeft <= 0)
                        {
                            orderLine.Status = "Rejected";
                            orderLine.RejectionReason = "Repeats exceeded - Please consult your doctor for a new prescription";
                            orderLinesToReject.Add(orderLine);
                            continue;
                        }

                        if (orderLine.Medications.QuantityOnHand < orderLine.Quantity)
                        {
                            orderLine.Status = "Rejected";
                            orderLine.RejectionReason = "Insufficient stock - We are restocking, please check back later";
                            orderLinesToReject.Add(orderLine);
                            continue;
                        }

                        orderLinesToDispense.Add(orderLine);
                    }

                    foreach (var orderLine in orderLinesToDispense)
                    {
                        orderLine.Medications.QuantityOnHand -= orderLine.Quantity;

                        if (orderLine.ScriptLine != null && orderLine.ScriptLine.Repeats > 0 && orderLine.ScriptLine.RepeatsLeft > 0)
                        {
                            orderLine.ScriptLine.RepeatsLeft--;
                        }

                        orderLine.Status = "Dispensed";
                    }

                    if (order.PharmacistID == null)
                    {
                        order.PharmacistID = pharmacist.PharmacistID;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // ADDED: Send partial completion email if some items were dispensed
                    if (orderLinesToDispense.Count > 0)
                    {
                        SendPartialDispensingEmail(order, orderLinesToDispense, orderLinesToReject);
                    }

                    var resultMessage = "";
                    if (orderLinesToDispense.Count > 0 && orderLinesToReject.Count > 0)
                    {
                        resultMessage = $"{orderLinesToDispense.Count} medication(s) dispensed successfully! {orderLinesToReject.Count} medication(s) were automatically rejected (repeats exceeded or insufficient stock).";
                    }
                    else if (orderLinesToDispense.Count > 0)
                    {
                        resultMessage = $"{orderLinesToDispense.Count} medication(s) dispensed successfully!";
                    }
                    else if (orderLinesToReject.Count > 0)
                    {
                        resultMessage = $"{orderLinesToReject.Count} medication(s) were automatically rejected (repeats exceeded or insufficient stock).";
                    }
                    else
                    {
                        resultMessage = "No medications were processed.";
                    }

                    return Json(new
                    {
                        success = true,
                        message = resultMessage,
                        dispensedCount = orderLinesToDispense.Count,
                        rejectedCount = orderLinesToReject.Count
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
                    .Include(ol => ol.ScriptLine)
                    .Include(ol => ol.Medications)
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
                    orderLine.Status = "Rejected";
                    orderLine.RejectionReason = rejectionReason.Trim();

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

        // POST: Complete order processing - UPDATED to redirect to CollectionTracking
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
                    .Include(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.Medications)
                    .Include(o => o.OrderLines)
                        .ThenInclude(ol => ol.ScriptLine)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Order not found";
                    return RedirectToAction(nameof(Index));
                }

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
                    var dispensedLines = order.OrderLines.Count(ol => ol.Status == "Dispensed");
                    var rejectedLines = order.OrderLines.Count(ol => ol.Status == "Rejected");

                    if (dispensedLines > 0)
                    {
                        order.Status = "Ready for Collection";
                        // ADDED: Send order ready email
                        SendOrderReadyEmail(order);
                        TempData["SuccessMessage"] = $"Order {order.OrderNumber} is ready for collection!";
                    }
                    else if (rejectedLines == order.OrderLines.Count)
                    {
                        order.Status = "Waiting Customer Action";
                        // ADDED: Send customer action required email
                        SendCustomerActionRequiredEmail(order);
                        TempData["WarningMessage"] = $"Order {order.OrderNumber} requires customer action. All medications were rejected.";
                    }

                    if (order.PharmacistID == null)
                    {
                        order.PharmacistID = pharmacist.PharmacistID;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // REDIRECT TO COLLECTION TRACKING INSTEAD OF INDEX
                    return RedirectToAction(nameof(CollectionTracking));
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

        // =========================================================================
        // ENHANCED EMAIL METHODS WITH COMPLETE ORDER DETAILS & HIGHLIGHTING
        // =========================================================================

        /// <summary>
        /// Send email when order is ready for collection
        /// </summary>
        private void SendOrderReadyEmail(Order order)
        {
            try
            {
                var receiver = order.Customer?.ApplicationUser?.Email;
                if (string.IsNullOrEmpty(receiver)) return;

                var subject = $"💊 Your Medications Are Ready - {order.OrderNumber} - IbhayiPharmacy-GRP-04-14";

                // Get medication details for email
                var dispensedMeds = new List<string>();
                var rejectedMeds = new List<string>();
                decimal totalDispensedAmount = 0;

                if (order.OrderLines != null)
                {
                    foreach (var ol in order.OrderLines)
                    {
                        var medName = ol.Medications?.MedicationName ?? "Unknown Medication";
                        var instructions = ol.ScriptLine?.Instructions ?? "Take as directed";
                        var price = ol.ItemPrice;
                        var lineTotal = price * ol.Quantity;

                        if (ol.Status == "Dispensed")
                        {
                            totalDispensedAmount += lineTotal;
                            dispensedMeds.Add($@"
                        <div style='background-color: #e8f5e8; border-left: 4px solid #27ae60; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                            <strong style='color: #27ae60; font-size: 16px;'>🟢 {medName}</strong><br>
                            <strong>Quantity:</strong> {ol.Quantity} | <strong>Price:</strong> R {price:F2} each<br>
                            <strong>Line Total:</strong> R {lineTotal:F2}<br>
                            <strong>Instructions:</strong> {instructions}
                        </div>");
                        }
                        else if (ol.Status == "Rejected")
                        {
                            rejectedMeds.Add($@"
                        <div style='background-color: #ffeaea; border-left: 4px solid #e74c3c; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                            <strong style='color: #e74c3c;'>🔴 {medName}</strong><br>
                            <strong>Reason:</strong> {ol.RejectionReason ?? "Not specified"}<br>
                            <strong>Instructions:</strong> {instructions}
                        </div>");
                        }
                    }
                }

                var message = $@"
            <h3>💊 Your Medications Are Ready for Collection</h3>
            <p>Dear {order.Customer?.ApplicationUser?.Name}, your order <strong>{order.OrderNumber}</strong> is ready for collection!</p>
            
            <div style='background-color: #f0f8ff; padding: 20px; border-radius: 8px; margin: 20px 0; border: 2px solid #3498db;'>
                <h4 style='color: #3498db; margin-top: 0;'>📦 Order Summary</h4>
                <p><strong>Order Number:</strong> {order.OrderNumber}</p>
                <p><strong>Ready Since:</strong> {DateTime.Now:dd MMMM yyyy HH:mm}</p>
                <p><strong>Dispensed Amount:</strong> <strong style='color: #27ae60;'>R {totalDispensedAmount:F2}</strong></p>
                <p><strong>Total Order Amount:</strong> R {order.TotalDue}</p>
                <p><strong>Processed By:</strong> {order.Pharmacist?.ApplicationUser?.Name ?? "Pharmacy Team"}</p>
            </div>";

                if (dispensedMeds.Any())
                {
                    message += @"<h4 style='color: #27ae60;'>✅ MEDICATIONS READY FOR COLLECTION:</h4>";
                    foreach (var med in dispensedMeds)
                    {
                        message += med;
                    }
                }

                if (rejectedMeds.Any())
                {
                    message += @"<h4 style='color: #e74c3c;'>❌ MEDICATIONS NOT AVAILABLE:</h4>";
                    foreach (var med in rejectedMeds)
                    {
                        message += med;
                    }

                    message += $@"
                <div style='background-color: #fff3cd; padding: 15px; border-radius: 5px; margin: 15px 0; border-left: 4px solid #f39c12;'>
                    <h4 style='color: #856404; margin-top: 0;'>👨‍⚕️ Important Notice</h4>
                    <p>For medications that could not be dispensed, please <strong>consult your doctor</strong> to discuss alternative treatment options or new prescriptions if needed.</p>
                </div>";
                }

                message += $@"
            <div style='background-color: #e8f4fd; padding: 18px; border-radius: 6px; margin: 20px 0; border: 1px solid #3498db;'>
                <h4 style='color: #2980b9; margin-top: 0;'>🛎️ COLLECTION INSTRUCTIONS</h4>
                <ul style='margin-bottom: 0;'>
                    <li>Bring your <strong>ID document</strong> or driver's license</li>
                    <li>Bring your <strong>medical aid card</strong> if applicable</li>
                    <li>Collection available during pharmacy hours</li>
                    <li>Please collect within <strong>3 days</strong></li>
                </ul>
            </div>

            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                <h4 style='color: #2c3e50; margin-top: 0;'>📍 COLLECTION ADDRESS</h4>
                <p style='margin-bottom: 5px;'><strong>Ibhayi Pharmacy</strong></p>
                <p style='margin-bottom: 5px;'>123 Govan Mbeki Avenue</p>
                <p style='margin-bottom: 5px;'>Port Elizabeth, 6001</p>
                <p style='margin-bottom: 0;'>📞 041 123 4567</p>
            </div>

            <p>Thank you for choosing Ibhayi Pharmacy! 🏥</p>
            
            <div style='border-top: 2px solid #3498db; padding-top: 15px; margin-top: 20px;'>
                <p style='margin-bottom: 5px;'>Regards,</p>
                <p style='margin-bottom: 5px; font-weight: bold;'>{order.Pharmacist?.ApplicationUser?.Name ?? "The Pharmacy Team"}</p>
                <p style='margin-bottom: 0; font-weight: bold; color: #3498db;'>Ibhayi Pharmacy</p>
            </div>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch
            {
                // Email failure shouldn't break the dispensing process
            }
        }

        /// <summary>
        /// Send email when customer collects their order
        /// </summary>
        private void SendCollectionConfirmationEmail(Order order)
        {
            try
            {
                var receiver = order.Customer?.ApplicationUser?.Email;
                if (string.IsNullOrEmpty(receiver)) return;

                var subject = $"✅ Collection Confirmed - {order.OrderNumber} - IbhayiPharmacy-GRP-04-14";

                // Get collected medications for the email
                var collectedMeds = new List<string>();
                decimal totalCollectedAmount = 0;

                if (order.OrderLines != null)
                {
                    foreach (var ol in order.OrderLines.Where(ol => ol.Status == "Dispensed"))
                    {
                        var medName = ol.Medications?.MedicationName ?? "Unknown Medication";
                        var instructions = ol.ScriptLine?.Instructions ?? "Take as directed";
                        var price = ol.ItemPrice;
                        var lineTotal = price * ol.Quantity;
                        totalCollectedAmount += lineTotal;

                        collectedMeds.Add($@"
                    <div style='background-color: #e8f5e8; border-left: 4px solid #27ae60; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                        <strong style='color: #27ae60; font-size: 16px;'>✅ {medName}</strong><br>
                        <strong>Quantity Collected:</strong> {ol.Quantity} | <strong>Price:</strong> R {price:F2} each<br>
                        <strong>Line Total:</strong> R {lineTotal:F2}<br>
                        <strong>Instructions:</strong> {instructions}
                    </div>");
                    }
                }

                var message = $@"
            <h3>✅ Medication Collection Confirmed</h3>
            <p>Dear {order.Customer?.ApplicationUser?.Name}, thank you for collecting your order!</p>
            
            <div style='background-color: #f0fff0; padding: 20px; border-radius: 8px; margin: 20px 0; border: 2px solid #27ae60;'>
                <h4 style='color: #27ae60; margin-top: 0;'>📦 Collection Summary</h4>
                <p><strong>Order Number:</strong> {order.OrderNumber}</p>
                <p><strong>Collected On:</strong> {DateTime.Now:dd MMMM yyyy HH:mm}</p>
                <p><strong>Total Collected:</strong> <strong style='color: #27ae60;'>R {totalCollectedAmount:F2}</strong></p>
                <p><strong>Dispensing Pharmacist:</strong> {order.Pharmacist?.ApplicationUser?.Name ?? "Pharmacy Team"}</p>
            </div>";

                if (collectedMeds.Any())
                {
                    message += @"<h4 style='color: #27ae60;'>💊 COLLECTED MEDICATIONS:</h4>";
                    foreach (var med in collectedMeds)
                    {
                        message += med;
                    }
                }

                message += $@"
            <div style='background-color: #e8f4fd; padding: 18px; border-radius: 6px; margin: 20px 0; border: 1px solid #3498db;'>
                <h4 style='color: #2980b9; margin-top: 0;'>💡 IMPORTANT MEDICATION REMINDERS</h4>
                <ul style='margin-bottom: 0;'>
                    <li>Follow the prescribed instructions carefully</li>
                    <li>Complete the full course of antibiotics if prescribed</li>
                    <li>Store medications as directed (room temperature/refrigerated)</li>
                    <li>Keep medications out of reach of children</li>
                    <li>Contact us immediately if you experience any side effects</li>
                </ul>
            </div>

            <div style='background-color: #fff3cd; padding: 15px; border-radius: 5px; margin: 15px 0; border-left: 4px solid #f39c12;'>
                <h4 style='color: #856404; margin-top: 0;'>📞 Need Assistance?</h4>
                <p>Our pharmacists are available to answer any questions about your medications at <strong>041 123 4567</strong>.</p>
            </div>

            <p>We hope you feel better soon! ❤️</p>
            
            <div style='border-top: 2px solid #27ae60; padding-top: 15px; margin-top: 20px;'>
                <p style='margin-bottom: 5px;'>Regards,</p>
                <p style='margin-bottom: 5px; font-weight: bold;'>{order.Pharmacist?.ApplicationUser?.Name ?? "The Pharmacy Team"}</p>
                <p style='margin-bottom: 0; font-weight: bold; color: #27ae60;'>Ibhayi Pharmacy</p>
            </div>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch
            {
                // Email failure shouldn't break the collection process
            }
        }

        /// <summary>
        /// Send email when order requires customer action
        /// </summary>
        private void SendCustomerActionRequiredEmail(Order order)
        {
            try
            {
                var receiver = order.Customer?.ApplicationUser?.Email;
                if (string.IsNullOrEmpty(receiver)) return;

                var subject = $"⚠️ Action Required - Order {order.OrderNumber} - IbhayiPharmacy-GRP-04-14";

                // Get rejected medications for the email
                var rejectedMeds = new List<string>();

                if (order.OrderLines != null)
                {
                    foreach (var ol in order.OrderLines.Where(ol => ol.Status == "Rejected"))
                    {
                        var medName = ol.Medications?.MedicationName ?? "Unknown Medication";
                        var instructions = ol.ScriptLine?.Instructions ?? "Take as directed";
                        var price = ol.ItemPrice;

                        rejectedMeds.Add($@"
                    <div style='background-color: #ffeaea; border-left: 4px solid #e74c3c; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                        <strong style='color: #e74c3c; font-size: 16px;'>❌ {medName}</strong><br>
                        <strong>Price:</strong> R {price:F2} each<br>
                        <strong>Reason:</strong> {ol.RejectionReason ?? "Not specified"}<br>
                        <strong>Instructions:</strong> {instructions}
                    </div>");
                    }
                }

                var message = $@"
            <h3>⚠️ Action Required: Order {order.OrderNumber}</h3>
            <p>Dear {order.Customer?.ApplicationUser?.Name}, we need to discuss your recent order.</p>
            
            <div style='background-color: #fffaf0; padding: 20px; border-radius: 8px; margin: 20px 0; border: 2px solid #e67e22;'>
                <h4 style='color: #e67e22; margin-top: 0;'>📦 Order Status Update</h4>
                <p><strong>Order Number:</strong> {order.OrderNumber}</p>
                <p><strong>Order Date:</strong> {order.OrderDate:dd MMMM yyyy}</p>
                <p><strong>Status:</strong> <strong style='color: #e74c3c;'>Requires Customer Action</strong></p>
                <p><strong>Pharmacist:</strong> {order.Pharmacist?.ApplicationUser?.Name ?? "Pharmacy Team"}</p>
            </div>";

                if (rejectedMeds.Any())
                {
                    message += @"<h4 style='color: #e74c3c;'>❌ MEDICATIONS NOT DISPENSED:</h4>";
                    foreach (var med in rejectedMeds)
                    {
                        message += med;
                    }
                }

                message += $@"
            <div style='background-color: #f8d7da; padding: 18px; border-radius: 6px; margin: 20px 0; border: 1px solid #e74c3c;'>
                <h4 style='color: #721c24; margin-top: 0;'>👨‍⚕️ URGENT: CONSULT YOUR DOCTOR</h4>
                <p><strong>None of your prescribed medications could be dispensed at this time.</strong></p>
                <p>Please <strong>consult your doctor immediately</strong> to discuss:</p>
                <ul style='margin-bottom: 0;'>
                    <li>Alternative medication options</li>
                    <li>New prescriptions if needed</li>
                    <li>Different treatment approaches</li>
                </ul>
            </div>

            <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                <h4 style='color: #2c3e50; margin-top: 0;'>📞 CONTACT US</h4>
                <p style='margin-bottom: 5px;'><strong>Ibhayi Pharmacy</strong></p>
                <p style='margin-bottom: 5px;'>123 Govan Mbeki Avenue, Port Elizabeth, 6001</p>
                <p style='margin-bottom: 5px;'>📞 041 123 4567</p>
                <p style='margin-bottom: 0;'><strong>Reference:</strong> {order.OrderNumber}</p>
            </div>

            <p>We apologize for any inconvenience and look forward to assisting you.</p>
            
            <div style='border-top: 2px solid #e74c3c; padding-top: 15px; margin-top: 20px;'>
                <p style='margin-bottom: 5px;'>Regards,</p>
                <p style='margin-bottom: 5px; font-weight: bold;'>{order.Pharmacist?.ApplicationUser?.Name ?? "The Pharmacy Team"}</p>
                <p style='margin-bottom: 0; font-weight: bold; color: #e74c3c;'>Ibhayi Pharmacy</p>
            </div>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch
            {
                // Email failure shouldn't break the order processing
            }
        }

        /// <summary>
        /// Send email when some medications are dispensed but order is not yet complete
        /// </summary>
        private void SendPartialDispensingEmail(Order order, List<OrderLine> dispensedLines, List<OrderLine> rejectedLines)
        {
            try
            {
                var receiver = order.Customer?.ApplicationUser?.Email;
                if (string.IsNullOrEmpty(receiver)) return;

                var subject = $"🔄 Partial Dispensing Update - {order.OrderNumber} - IbhayiPharmacy-GRP-04-14";

                var message = $@"
            <h3>🔄 Partial Dispensing Update</h3>
            <p>Dear {order.Customer?.ApplicationUser?.Name}, some medications from your order have been processed.</p>
            
            <div style='background-color: #fffaf0; padding: 20px; border-radius: 8px; margin: 20px 0; border: 2px solid #f39c12;'>
                <h4 style='color: #f39c12; margin-top: 0;'>📦 Processing Update</h4>
                <p><strong>Order Number:</strong> {order.OrderNumber}</p>
                <p><strong>Update Date:</strong> {DateTime.Now:dd MMMM yyyy HH:mm}</p>
                <p><strong>Status:</strong> Partially Processed</p>
                <p><strong>Pharmacist:</strong> {order.Pharmacist?.ApplicationUser?.Name ?? "Pharmacy Team"}</p>
            </div>";

                if (dispensedLines.Any())
                {
                    message += @"<h4 style='color: #27ae60;'>✅ MEDICATIONS SUCCESSFULLY DISPENSED:</h4>";
                    foreach (var line in dispensedLines)
                    {
                        var medName = line.Medications?.MedicationName ?? "Unknown Medication";
                        var price = line.ItemPrice;
                        var lineTotal = price * line.Quantity;

                        message += $@"
                    <div style='background-color: #e8f5e8; border-left: 4px solid #27ae60; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                        <strong style='color: #27ae60; font-size: 16px;'>🟢 {medName}</strong><br>
                        <strong>Quantity:</strong> {line.Quantity} | <strong>Price:</strong> R {price:F2} each<br>
                        <strong>Line Total:</strong> R {lineTotal:F2}<br>
                        <strong>Instructions:</strong> {line.ScriptLine?.Instructions ?? "Take as directed"}
                    </div>";
                    }
                }

                if (rejectedLines.Any())
                {
                    message += @"<h4 style='color: #e74c3c;'>❌ MEDICATIONS NOT AVAILABLE:</h4>";
                    foreach (var line in rejectedLines)
                    {
                        var medName = line.Medications?.MedicationName ?? "Unknown Medication";
                        var price = line.ItemPrice;

                        message += $@"
                    <div style='background-color: #ffeaea; border-left: 4px solid #e74c3c; padding: 12px; margin: 8px 0; border-radius: 4px;'>
                        <strong style='color: #e74c3c;'>🔴 {medName}</strong><br>
                        <strong>Price:</strong> R {price:F2} each<br>
                        <strong>Reason:</strong> {line.RejectionReason}<br>
                        <strong>Instructions:</strong> {line.ScriptLine?.Instructions ?? "Take as directed"}
                    </div>";
                    }
                }

                message += $@"
            <div style='background-color: #e8f4fd; padding: 15px; border-radius: 5px; margin: 15px 0; border: 1px solid #3498db;'>
                <h4 style='color: #2980b9; margin-top: 0;'>ℹ️ IMPORTANT NOTE</h4>
                <p>Your order is still being processed. You will receive another notification when all medications are ready for collection.</p>
            </div>

            <p>Thank you for your patience!</p>
            
            <div style='border-top: 2px solid #f39c12; padding-top: 15px; margin-top: 20px;'>
                <p style='margin-bottom: 5px;'>Regards,</p>
                <p style='margin-bottom: 5px; font-weight: bold;'>{order.Pharmacist?.ApplicationUser?.Name ?? "The Pharmacy Team"}</p>
                <p style='margin-bottom: 0; font-weight: bold; color: #f39c12;'>Ibhayi Pharmacy</p>
            </div>";

                _email.SendEmailAsync(receiver, subject, message);
            }
            catch
            {
                // Email failure shouldn't break the dispensing process
            }
        }
    }
    
}