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
    public class PharmacistDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmacistDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Main Pharmacist Dashboard
        public async Task<IActionResult> Index()
        {
            try
            {
                var pharmacistId = GetCurrentPharmacistId();

                if (string.IsNullOrEmpty(pharmacistId))
                {
                    return RedirectToAction("Login", "Account");
                }

                var dashboardData = new PharmacistDashboardVM
                {
                    // Orders requiring pharmacist attention
                    PendingOrders = await _context.Orders
                        .Where(o => o.Status == "Ordered")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderByDescending(o => o.OrderDate)
                        .Take(10)
                        .ToListAsync(),

                    // Orders ready for collection
                    ReadyForCollection = await _context.Orders
                        .Where(o => o.Status == "Ready for Collection")
                        .Include(o => o.Customer)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(o => o.OrderLines)
                        .OrderByDescending(o => o.OrderDate)
                        .Take(10)
                        .ToListAsync(),

                    // Unprocessed prescriptions
                    UnprocessedPrescriptions = await _context.Prescriptions
                        .Where(p => string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending")
                        .Include(p => p.ApplicationUser)
                        .OrderByDescending(p => p.DateIssued)
                        .Take(10)
                        .ToListAsync(),

                    // Low stock medications
                    LowStockMedications = await _context.Medications
                        .Where(m => m.QuantityOnHand <= m.ReOrderLevel)
                        .OrderBy(m => m.QuantityOnHand)
                        .Take(10)
                        .ToListAsync(),

                    // Statistics
                    TotalPendingOrders = await _context.Orders.CountAsync(o => o.Status == "Ordered"),
                    TotalReadyForCollection = await _context.Orders.CountAsync(o => o.Status == "Ready for Collection"),
                    TotalUnprocessedScripts = await _context.Prescriptions.CountAsync(p =>
                        string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending"),
                    TotalLowStockItems = await _context.Medications.CountAsync(m => m.QuantityOnHand <= m.ReOrderLevel),

                    // Today's activity
                    TodayProcessedOrders = await _context.Orders
                        .Where(o => o.OrderDate.Date == DateTime.Today &&
                                   (o.Status == "Ready for Collection" || o.Status == "Collected"))
                        .CountAsync(),

                    TodayProcessedScripts = await _context.Prescriptions
                        .Where(p => p.DateIssued.Date == DateTime.Today &&
                                   (p.Status == "Processed" || p.Status == "Partially Processed"))
                        .CountAsync()
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
        public async Task<IActionResult> QuickProcessPrescription()
        {
            try
            {
                var nextPrescription = await _context.Prescriptions
                    .Where(p => string.IsNullOrEmpty(p.Status) || p.Status == "Unprocessed" || p.Status == "Pending")
                    .Include(p => p.ApplicationUser)
                    .OrderBy(p => p.DateIssued)
                    .FirstOrDefaultAsync();

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
        public async Task<IActionResult> QuickProcessOrder()
        {
            try
            {
                var nextOrder = await _context.Orders
                    .Where(o => o.Status == "Ordered")
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .OrderBy(o => o.OrderDate)
                    .FirstOrDefaultAsync();

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
        public async Task<JsonResult> GetDashboardStats()
        {
            try
            {
                var today = DateTime.Today;
                var last7Days = today.AddDays(-6);

                // Orders by status
                var ordersByStatus = await _context.Orders
                    .GroupBy(o => o.Status)
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

                // Orders last 7 days
                var ordersLast7Days = await _context.Orders
                    .Where(o => o.OrderDate >= last7Days)
                    .GroupBy(o => o.OrderDate.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToListAsync();

                // Prescriptions by status
                var prescriptionsByStatus = await _context.Prescriptions
                    .GroupBy(p => p.Status ?? "Unprocessed")
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();

                // Low stock medications by urgency
                var stockLevels = await _context.Medications
                    .GroupBy(m => m.QuantityOnHand == 0 ? "Out of Stock" :
                                 m.QuantityOnHand <= 5 ? "Critical" :
                                 m.QuantityOnHand <= m.ReOrderLevel ? "Low" : "Normal")
                    .Select(g => new
                    {
                        Level = g.Key,
                        Count = g.Count()
                    })
                    .Where(x => x.Level != "Normal")
                    .ToListAsync();

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

        // API: Get recent activity
        [HttpGet]
        public async Task<JsonResult> GetRecentActivity()
        {
            try
            {
                var recentOrders = await _context.Orders
                    .Include(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .Select(o => new
                    {
                        Type = "Order",
                        Description = $"Order {o.OrderNumber} - {o.Customer.ApplicationUser.Name}",
                        Date = o.OrderDate,
                        Status = o.Status
                    })
                    .ToListAsync();

                var recentPrescriptions = await _context.Prescriptions
                    .Include(p => p.ApplicationUser)
                    .OrderByDescending(p => p.DateIssued)
                    .Take(5)
                    .Select(p => new
                    {
                        Type = "Prescription",
                        Description = $"Prescription - {p.ApplicationUser.Name}",
                        Date = p.DateIssued,
                        Status = p.Status ?? "Unprocessed"
                    })
                    .ToListAsync();

                var recentActivity = recentOrders
                    .Concat(recentPrescriptions)
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

        // Helper method to get current pharmacist ID
        private string GetCurrentPharmacistId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}