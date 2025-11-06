using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using IbhayiPharmacy.Models.PharmacyManagerVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Pharmacist")]
    public class PharmacistDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SmtpSettings _smtpSettings;
        private readonly EmailService _emailService;

        public PharmacistDashboardController(ApplicationDbContext context, IOptions<SmtpSettings> smtpSettings, EmailService emailService)
        {
            _context = context;
            _smtpSettings = smtpSettings.Value;
            _emailService = emailService;
        }

        // Main Pharmacist Dashboard
        public IActionResult Index()
        {
            try
            {
                var pharmacistId = GetCurrentPharmacistId();

                var dashboardData = new PharmacistDashboardVM
                {
                    // Orders requiring pharmacist attention
                    PendingOrders = _context.Orders
                        .Where(o => o.Status == "Ordered")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderByDescending(o => o.OrderDate)
                        .Take(10)
                        .ToList(),

                    // Orders ready for collection
                    ReadyForCollection = _context.Orders
                        .Where(o => o.Status == "Ready for Collection")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderByDescending(o => o.OrderDate)
                        .Take(10)
                        .ToList(),

                    // Unprocessed prescriptions
                    UnprocessedPrescriptions = _context.Prescriptions
                        .Where(p => string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending")
                        .Include(p => p.ApplicationUser)
                        .OrderByDescending(p => p.DateIssued)
                        .Take(10)
                        .ToList(),

                    // Low stock medications
                    LowStockMedications = _context.Medications
                        .Where(m => m.QuantityOnHand <= m.ReOrderLevel)
                        .OrderBy(m => m.QuantityOnHand)
                        .Take(10)
                        .ToList(),

                    // Statistics
                    TotalPendingOrders = _context.Orders.Count(o => o.Status == "Ordered"),
                    TotalReadyForCollection = _context.Orders.Count(o => o.Status == "Ready for Collection"),
                    TotalUnprocessedScripts = _context.Prescriptions.Count(p =>
                        string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending"),
                    TotalLowStockItems = _context.Medications.Count(m => m.QuantityOnHand <= m.ReOrderLevel),

                    // Today's activity
                    TodayProcessedOrders = _context.Orders
                        .Count(o => o.OrderDate.Date == DateTime.Today &&
                                   (o.Status == "Ready for Collection" || o.Status == "Collected")),
                    TodayProcessedScripts = _context.Prescriptions
                        .Count(p => p.DateIssued.Date == DateTime.Today &&
                                   (p.Status == "Processed" || p.Status == "Partially Processed"))
                };

                return View(dashboardData);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading dashboard: {ex.Message}";
                return View(new PharmacistDashboardVM());
            }
        }

        // GET: Quick Actions - Process Prescription
        public IActionResult QuickProcessPrescription()
        {
            try
            {
                var nextPrescription = _context.Prescriptions
                    .Where(p => string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending")
                    .Include(p => p.ApplicationUser)
                    .OrderBy(p => p.DateIssued)
                    .FirstOrDefault();

                if (nextPrescription != null)
                {
                    return RedirectToAction("Edit", "ScriptsProcessed", new { id = nextPrescription.PrescriptionID });
                }

                TempData["InfoMessage"] = "No unprocessed prescriptions found.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading next prescription: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Quick Actions - Process Order
        public IActionResult QuickProcessOrder()
        {
            try
            {
                var nextOrder = _context.Orders
                    .Where(o => o.Status == "Ordered")
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .OrderBy(o => o.OrderDate)
                    .FirstOrDefault();

                if (nextOrder != null)
                {
                    return RedirectToAction("DispenseOrder", "PharmacistDispensing", new { id = nextOrder.OrderID });
                }

                TempData["InfoMessage"] = "No pending orders found.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading next order: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Quick Actions - View Collection Orders
        public IActionResult QuickViewCollections()
        {
            return RedirectToAction("CollectionTracking", "PharmacistDispensing");
        }

        // GET: Quick Actions - View Processed Scripts
        public IActionResult QuickViewProcessedScripts()
        {
            return RedirectToAction("ProcessedScripts", "ScriptsProcessed");
        }

        // API: Get dashboard statistics for charts
        [HttpGet]
        public JsonResult GetDashboardStats()
        {
            try
            {
                var today = DateTime.Today;
                var last7Days = today.AddDays(-6);

                // Orders by status
                var ordersByStatus = _context.Orders
                    .GroupBy(o => o.Status)
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                // Orders last 7 days
                var ordersLast7Days = _context.Orders
                    .Where(o => o.OrderDate >= last7Days)
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                // Prescriptions by status
                var prescriptionsByStatus = _context.Prescriptions
                    .GroupBy(p => p.Status ?? "Unprocessed")
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                // Low stock medications by urgency
                var stockLevels = _context.Medications
                    .GroupBy(m => m.QuantityOnHand == 0 ? "Out of Stock" :
                                 m.QuantityOnHand <= 5 ? "Critical" :
                                 m.QuantityOnHand <= m.ReOrderLevel ? "Low" : "Normal")
                    .Select(g => new
                    {
                        Level = g.Key,
                        Count = g.Count()
                    })
                    .Where(x => x.Level != "Normal")
                    .ToList();

                return Json(new
                {
                    success = true,
                    ordersByStatus,
                    ordersLast7Days,
                    prescriptionsByStatus,
                    stockLevels
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // API: Get recent activity - OPTIMIZED VERSION
        [HttpGet]
        public JsonResult GetRecentActivity()
        {
            try
            {
                // Get recent processed prescriptions (what pharmacist actually worked on)
                var processedScripts = _context.Prescriptions
                    .Where(p => p.Status == "Processed" || p.Status == "Partially Processed")
                    .OrderByDescending(p => p.DateIssued)
                    .Take(5)
                    .Select(p => new
                    {
                        Type = "Prescription Processed",
                        Description = $"Processed prescription for {p.ApplicationUser.Name} {p.ApplicationUser.Surname}",
                        Date = p.DateIssued,
                        Status = p.Status,
                        Icon = "✅"
                    })
                    .ToList();

                // Get recent dispensed orders (what pharmacist actually dispensed)
                var dispensedOrders = _context.Orders
                    .Where(o => o.Status == "Ready for Collection" || o.Status == "Collected")
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .Select(o => new
                    {
                        Type = "Order Dispensed",
                        Description = $"Dispensed order {o.OrderNumber} for {o.Customer.ApplicationUser.Name} {o.Customer.ApplicationUser.Surname}",
                        Date = o.OrderDate,
                        Status = o.Status,
                        Icon = "📦"
                    })
                    .ToList();

                // Combine and get top 10 most recent
                var recentActivity = processedScripts
                    .Concat(dispensedOrders)
                    .OrderByDescending(x => x.Date)
                    .Take(10)
                    .ToList();

                return Json(new { success = true, activity = recentActivity });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // NEW: Get new items count for notifications
        [HttpGet]
        public JsonResult GetNewItemsCount()
        {
            try
            {
                var newOrders = _context.Orders
                    .Count(o => o.Status == "Ordered" && o.OrderDate.Date == DateTime.Today);

                var newScripts = _context.Prescriptions
                    .Count(p => (string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed") &&
                               p.DateIssued.Date == DateTime.Today);

                return Json(new { success = true, newOrders, newScripts });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // NEW: Notify Manager about low stock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult NotifyManager()
        {
            try
            {
                var pharmacistId = GetCurrentPharmacistId();
                var pharmacist = _context.Pharmacists
                    .Include(p => p.ApplicationUser)
                    .FirstOrDefault(p => p.ApplicationUserId == pharmacistId);

                var pharmacistName = pharmacist?.ApplicationUser?.Name ?? "Pharmacist";

                // Get all low stock medications
                var lowStockMedications = _context.Medications
                    .Where(m => m.QuantityOnHand <= m.ReOrderLevel)
                    .OrderBy(m => m.QuantityOnHand)
                    .ToList();

                if (!lowStockMedications.Any())
                {
                    return Json(new { success = false, message = "No low stock items found to report." });
                }

                // Get manager email (you might want to get this from your database)
                var managerEmail = "mbasamajila001@gmail.com"; // Replace with actual manager email from database

                // Create email content
                var emailSubject = $"Low Stock Alert - {DateTime.Now:dd MMM yyyy HH:mm}";
                var emailBody = GenerateLowStockEmailBody(pharmacistName, lowStockMedications);

                // Send email
                _emailService.SendEmailAsync(managerEmail, emailSubject, emailBody);

                // Log the notification (optional)
                LogNotification(pharmacistName, lowStockMedications.Count);

                return Json(new
                {
                    success = true,
                    message = $"Manager notified successfully. {lowStockMedications.Count} low stock items reported."
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error notifying manager: {ex.Message}" });
            }
        }

        // NEW: Notify Custom Email about low stock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult NotifyCustom([FromBody] CustomNotificationRequest request)
        {
            try
            {
                var pharmacistId = GetCurrentPharmacistId();
                var pharmacist = _context.Pharmacists
                    .Include(p => p.ApplicationUser)
                    .FirstOrDefault(p => p.ApplicationUserId == pharmacistId);

                var pharmacistName = pharmacist?.ApplicationUser?.Name ?? "Pharmacist";

                // Get all low stock medications
                var lowStockMedications = _context.Medications
                    .Where(m => m.QuantityOnHand <= m.ReOrderLevel)
                    .OrderBy(m => m.QuantityOnHand)
                    .ToList();

                if (!lowStockMedications.Any())
                {
                    return Json(new { success = false, message = "No low stock items found to report." });
                }

                // Validate email
                if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
                {
                    return Json(new { success = false, message = "Please provide a valid email address." });
                }

                // Create email content with custom options
                var emailSubject = $"Low Stock Report - {DateTime.Now:dd MMM yyyy HH:mm}";
                var emailBody = GenerateCustomLowStockEmailBody(pharmacistName, lowStockMedications, request);

                // Send email
                _emailService.SendEmailAsync(request.Email, emailSubject, emailBody);

                // Log the custom notification
                LogCustomNotification(pharmacistName, request.Email, lowStockMedications.Count);

                return Json(new
                {
                    success = true,
                    message = $"Low stock report sent successfully to {request.Email}"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending notification: {ex.Message}" });
            }
        }

        // NEW: Mark order as ready for collection
        [HttpPost]
        public JsonResult MarkOrderReady(int orderId)
        {
            try
            {
                var order = _context.Orders.Find(orderId);
                if (order == null)
                {
                    return Json(new { success = false, message = "Order not found." });
                }

                order.Status = "Ready for Collection";
                _context.SaveChanges();

                return Json(new { success = true, message = "Order marked as ready for collection." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error updating order: {ex.Message}" });
            }
        }

        // NEW: Get performance metrics
        [HttpGet]
        public JsonResult GetPerformanceMetrics()
        {
            try
            {
                var today = DateTime.Today;

                // Calculate average processing time (simplified)
                var processedOrders = _context.Orders
                    .Where(o => o.Status == "Ready for Collection" && o.OrderDate.Date == today)
                    .ToList();

                var avgProcessTime = processedOrders.Any() ? "15m" : "0m";

                // Calculate completion rate
                var totalOrdersToday = _context.Orders.Count(o => o.OrderDate.Date == today);
                var completedOrdersToday = _context.Orders.Count(o =>
                    o.OrderDate.Date == today &&
                    (o.Status == "Ready for Collection" || o.Status == "Collected"));

                var completionRate = totalOrdersToday > 0
                    ? $"{Math.Round((completedOrdersToday / (double)totalOrdersToday) * 100)}%"
                    : "100%";

                return Json(new
                {
                    success = true,
                    avgProcessTime,
                    completionRate
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Helper method to generate email body for low stock alert
        private string GenerateLowStockEmailBody(string pharmacistName, List<Medication> lowStockMedications)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; color: #333; }");
            sb.AppendLine(".header { background: #e74c3c; color: white; padding: 20px; text-align: center; }");
            sb.AppendLine(".content { padding: 20px; }");
            sb.AppendLine(".stock-table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            sb.AppendLine(".stock-table th, .stock-table td { border: 1px solid #ddd; padding: 12px; text-align: left; }");
            sb.AppendLine(".stock-table th { background: #f8f9fa; }");
            sb.AppendLine(".critical { background: #ffe6e6; color: #c0392b; font-weight: bold; }");
            sb.AppendLine(".warning { background: #fff3cd; color: #856404; }");
            sb.AppendLine(".footer { background: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #666; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"<div class='header'>");
            sb.AppendLine($"<h1>🚨 Low Stock Alert - Ibhayi Pharmacy</h1>");
            sb.AppendLine($"<p>Generated by: {pharmacistName} on {DateTime.Now:dd MMM yyyy 'at' HH:mm}</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class='content'>");
            sb.AppendLine("<h2>Low Stock Medications Report</h2>");
            sb.AppendLine("<p>The following medications are running low and require attention:</p>");
            sb.AppendLine("<table class='stock-table'>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Medication Name</th>");
            sb.AppendLine("<th>Current Stock</th>");
            sb.AppendLine("<th>Reorder Level</th>");
            sb.AppendLine("<th>Status</th>");
            sb.AppendLine("<th>Urgency</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var med in lowStockMedications)
            {
                var status = med.QuantityOnHand == 0 ? "Out of Stock" :
                            med.QuantityOnHand <= 5 ? "Critical" : "Low Stock";

                var urgencyClass = med.QuantityOnHand == 0 ? "critical" :
                                 med.QuantityOnHand <= 5 ? "critical" : "warning";

                var urgencyText = med.QuantityOnHand == 0 ? "🆘 URGENT - OUT OF STOCK" :
                                med.QuantityOnHand <= 5 ? "⚠️ CRITICAL - VERY LOW" : "⚠️ LOW STOCK";

                sb.AppendLine($"<tr class='{urgencyClass}'>");
                sb.AppendLine($"<td><strong>{med.MedicationName}</strong></td>");
                sb.AppendLine($"<td>{med.QuantityOnHand}</td>");
                sb.AppendLine($"<td>{med.ReOrderLevel}</td>");
                sb.AppendLine($"<td>{status}</td>");
                sb.AppendLine($"<td>{urgencyText}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            sb.AppendLine("<div style='margin-top: 30px; padding: 15px; background: #e8f4fd; border-radius: 5px;'>");
            sb.AppendLine("<h3>📊 Summary</h3>");
            sb.AppendLine($"<p><strong>Total Low Stock Items:</strong> {lowStockMedications.Count}</p>");
            sb.AppendLine($"<p><strong>Out of Stock:</strong> {lowStockMedications.Count(m => m.QuantityOnHand == 0)}</p>");
            sb.AppendLine($"<p><strong>Critical (≤5 units):</strong> {lowStockMedications.Count(m => m.QuantityOnHand > 0 && m.QuantityOnHand <= 5)}</p>");
            sb.AppendLine($"<p><strong>Low Stock:</strong> {lowStockMedications.Count(m => m.QuantityOnHand > 5 && m.QuantityOnHand <= m.ReOrderLevel)}</p>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div style='margin-top: 20px; padding: 15px; background: #fff3cd; border-radius: 5px;'>");
            sb.AppendLine("<p><strong>💡 Recommended Action:</strong> Please review these stock levels and place necessary orders to avoid running out of critical medications.</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='footer'>");
            sb.AppendLine("<p>This email was automatically generated by the Ibhayi Pharmacy System</p>");
            sb.AppendLine($"<p>© {DateTime.Now.Year} Ibhayi Pharmacy. All rights reserved.</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        // Helper method to generate custom email body with options
        private string GenerateCustomLowStockEmailBody(string pharmacistName, List<Medication> lowStockMedications, CustomNotificationRequest request)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; color: #333; }");
            sb.AppendLine(".header { background: #9b59b6; color: white; padding: 20px; text-align: center; }");
            sb.AppendLine(".content { padding: 20px; }");
            sb.AppendLine(".stock-table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            sb.AppendLine(".stock-table th, .stock-table td { border: 1px solid #ddd; padding: 12px; text-align: left; }");
            sb.AppendLine(".stock-table th { background: #f8f9fa; }");
            sb.AppendLine(".critical { background: #ffe6e6; color: #c0392b; font-weight: bold; }");
            sb.AppendLine(".warning { background: #fff3cd; color: #856404; }");
            sb.AppendLine(".footer { background: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #666; }");
            sb.AppendLine(".note { background: #e8f4fd; padding: 15px; border-radius: 5px; margin: 15px 0; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"<div class='header'>");
            sb.AppendLine($"<h1>📋 Low Stock Report - Ibhayi Pharmacy</h1>");
            sb.AppendLine($"<p>Generated by: {pharmacistName} on {DateTime.Now:dd MMM yyyy 'at' HH:mm}</p>");
            sb.AppendLine($"<p><em>This report was sent to you by request</em></p>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class='content'>");
            sb.AppendLine("<h2>Low Stock Medications</h2>");
            sb.AppendLine("<p>The following medications are currently running low:</p>");
            sb.AppendLine("<table class='stock-table'>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Medication Name</th>");
            sb.AppendLine("<th>Current Stock</th>");
            sb.AppendLine("<th>Reorder Level</th>");
            sb.AppendLine("<th>Status</th>");

            if (request.HighlightCritical)
            {
                sb.AppendLine("<th>Urgency</th>");
            }

            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var med in lowStockMedications)
            {
                var status = med.QuantityOnHand == 0 ? "Out of Stock" :
                            med.QuantityOnHand <= 5 ? "Critical" : "Low Stock";

                var urgencyClass = med.QuantityOnHand == 0 ? "critical" :
                                 med.QuantityOnHand <= 5 ? "critical" : "warning";

                sb.AppendLine($"<tr class='{urgencyClass}'>");
                sb.AppendLine($"<td><strong>{med.MedicationName}</strong></td>");
                sb.AppendLine($"<td>{med.QuantityOnHand}</td>");
                sb.AppendLine($"<td>{med.ReOrderLevel}</td>");
                sb.AppendLine($"<td>{status}</td>");

                if (request.HighlightCritical)
                {
                    var urgencyText = med.QuantityOnHand == 0 ? "🆘 URGENT - OUT OF STOCK" :
                                    med.QuantityOnHand <= 5 ? "⚠️ CRITICAL - VERY LOW" : "⚠️ LOW STOCK";
                    sb.AppendLine($"<td>{urgencyText}</td>");
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            // Include summary if requested
            if (request.IncludeSummary)
            {
                sb.AppendLine("<div class='note'>");
                sb.AppendLine("<h3>📊 Summary Statistics</h3>");
                sb.AppendLine($"<p><strong>Total Low Stock Items:</strong> {lowStockMedications.Count}</p>");
                sb.AppendLine($"<p><strong>Out of Stock:</strong> {lowStockMedications.Count(m => m.QuantityOnHand == 0)}</p>");
                sb.AppendLine($"<p><strong>Critical (≤5 units):</strong> {lowStockMedications.Count(m => m.QuantityOnHand > 0 && m.QuantityOnHand <= 5)}</p>");
                sb.AppendLine($"<p><strong>Low Stock:</strong> {lowStockMedications.Count(m => m.QuantityOnHand > 5 && m.QuantityOnHand <= m.ReOrderLevel)}</p>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("<div style='margin-top: 20px; padding: 15px; background: #fff3cd; border-radius: 5px;'>");
            sb.AppendLine("<p><strong>💡 Recommended Action:</strong> Please review these stock levels and place necessary orders to avoid running out of critical medications.</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<div class='footer'>");
            sb.AppendLine("<p>This email was automatically generated by the Ibhayi Pharmacy System</p>");
            sb.AppendLine($"<p>Sent to: {request.Email}</p>");
            sb.AppendLine($"<p>© {DateTime.Now.Year} Ibhayi Pharmacy. All rights reserved.</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Helper method to log notifications
        private void LogNotification(string pharmacistName, int lowStockCount)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] Low stock notification sent by {pharmacistName} for {lowStockCount} items");
        }

        // Helper method to log custom notifications
        private void LogCustomNotification(string pharmacistName, string recipientEmail, int lowStockCount)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] Custom low stock notification sent by {pharmacistName} to {recipientEmail} for {lowStockCount} items");
        }

        // Helper method to get current pharmacist ID
        private string GetCurrentPharmacistId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }

    // Request model for custom notifications
    public class CustomNotificationRequest
    {
        public string Email { get; set; } = string.Empty;
        public bool IncludeSummary { get; set; }
        public bool HighlightCritical { get; set; }
    }
}