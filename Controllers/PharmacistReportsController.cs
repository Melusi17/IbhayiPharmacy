///********************************************************************************************
// * 💊 Ibhayi Pharmacy - Pharmacist Report Module (PharmacistReportController.cs)
// * ------------------------------------------------------------------------------------------
// * 📦 PURPOSE:
// * Generates PDF reports showing medications dispensed by pharmacists grouped by CUSTOMER
// * Uses QuestPDF (modern .NET 8) - safe alongside iTextSharp in other controllers
// * 
// * ⚙️ COMPATIBILITY:
// * ✅ QuestPDF works with .NET 8 (iTextSharp doesn't)
// * ✅ No dependency conflicts with existing iTextSharp code
// * ✅ Uses your exact database schema (Orders, OrderLines, Medications, Customers)
// * 
// * 🎯 ONP400 REQUIREMENTS:
// * ✅ Proper PDF formatting with headers/footers
// * ✅ Page numbers on all pages
// * ✅ Report title in header from page 2 onwards
// * ✅ Grouped by Customer with subtotals
// * ✅ Grand totals at the end
// * ✅ Appropriate section headings and sorting
// * 
// * 🧩 SAFETY NOTES:
// * ✅ QuestPDF and iTextSharp use separate namespaces
// * ✅ No need to remove iTextSharp from project
// * ✅ This controller ONLY uses QuestPDF
// * 
// * Authored by: Melusi Mamba & Team
// * For: GRP-04-14 Ibhayi Pharmacy Project
// ********************************************************************************************/

//using IbhayiPharmacy.Data;
//using IbhayiPharmacy.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;
//using System.Security.Claims;

//namespace IbhayiPharmacy.Controllers
//{
//    [Authorize(Policy = "Pharmacist")]
//    public class PharmacistReportController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public PharmacistReportController(ApplicationDbContext context)
//        {
//            _context = context;
//            QuestPDF.Settings.License = LicenseType.Community;
//        }

//        // GET: Report filter page
//        public IActionResult Index()
//        {
//            var defaultStartDate = DateTime.Today.AddDays(-30);
//            var defaultEndDate = DateTime.Today;

//            ViewBag.DefaultStartDate = defaultStartDate.ToString("yyyy-MM-dd");
//            ViewBag.DefaultEndDate = defaultEndDate.ToString("yyyy-MM-dd");

//            return View();
//        }

//        // POST: Generate pharmacist report
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> GeneratePharmacistReport(DateTime startDate, DateTime endDate, string groupBy = "Customer")
//        {
//            try
//            {
//                // Validate date range
//                if (startDate > endDate)
//                {
//                    TempData["ErrorMessage"] = "Start date cannot be after end date.";
//                    return RedirectToAction(nameof(Index));
//                }

//                if (endDate > DateTime.Today)
//                {
//                    TempData["ErrorMessage"] = "End date cannot be in the future.";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Get current pharmacist
//                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                var pharmacist = await _context.Pharmacists
//                    .Include(p => p.ApplicationUser)
//                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

//                if (pharmacist == null)
//                {
//                    TempData["ErrorMessage"] = "Pharmacist profile not found.";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Fetch dispensed orders for this pharmacist within date range
//                var reportData = await _context.Orders
//                    .Include(o => o.Customer)
//                        .ThenInclude(c => c.ApplicationUser)
//                    .Include(o => o.OrderLines)
//                        .ThenInclude(ol => ol.Medications)
//                    .Include(o => o.Pharmacist)
//                        .ThenInclude(p => p.ApplicationUser)
//                    .Where(o => o.PharmacistID == pharmacist.PharmacistID &&
//                               o.OrderDate.Date >= startDate.Date &&
//                               o.OrderDate.Date <= endDate.Date &&
//                               o.OrderLines.Any(ol => ol.Status == "Dispensed"))
//                    .OrderBy(o => o.OrderDate)
//                    .ToListAsync();

//                if (!reportData.Any())
//                {
//                    TempData["WarningMessage"] = "No dispensed medications found for the selected date range.";
//                    return RedirectToAction(nameof(Index));
//                }

//                // Generate PDF based on grouping preference
//                var document = new PharmacistReportDocument(reportData, startDate, endDate, pharmacist, groupBy);

//                // ✅ FIXED: Proper PDF generation without CreateContainer
//                var pdfBytes = Document.Create(container => document.Compose(container)).GeneratePdf();

//                var fileName = $"PharmacistReport_{pharmacist.ApplicationUser.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
//                return File(pdfBytes, "application/pdf", fileName);
//            }
//            catch (Exception ex)
//            {
//                // ✅ FIXED: Variable 'ex' is now used
//                TempData["ErrorMessage"] = $"Error generating report: {ex.Message}";
//                return RedirectToAction(nameof(Index));
//            }
//        }

//        // API: Get report statistics for dashboard
//        [HttpGet]
//        public async Task<JsonResult> GetReportStats()
//        {
//            try
//            {
//                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//                var pharmacist = await _context.Pharmacists
//                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

//                if (pharmacist == null)
//                {
//                    return Json(new { error = "Pharmacist not found" });
//                }

//                var today = DateTime.Today;
//                var weekStart = today.AddDays(-(int)today.DayOfWeek);
//                var monthStart = new DateTime(today.Year, today.Month, 1);

//                var stats = new
//                {
//                    TodayDispensed = await _context.OrderLines
//                        .CountAsync(ol => ol.Order.PharmacistID == pharmacist.PharmacistID &&
//                                        ol.Order.OrderDate.Date == today &&
//                                        ol.Status == "Dispensed"),

//                    ThisWeekDispensed = await _context.OrderLines
//                        .CountAsync(ol => ol.Order.PharmacistID == pharmacist.PharmacistID &&
//                                        ol.Order.OrderDate >= weekStart &&
//                                        ol.Status == "Dispensed"),

//                    ThisMonthDispensed = await _context.OrderLines
//                        .CountAsync(ol => ol.Order.PharmacistID == pharmacist.PharmacistID &&
//                                        ol.Order.OrderDate >= monthStart &&
//                                        ol.Status == "Dispensed"),

//                    TotalCustomers = await _context.Orders
//                        .Where(o => o.PharmacistID == pharmacist.PharmacistID)
//                        .Select(o => o.CustomerID)
//                        .Distinct()
//                        .CountAsync()
//                };

//                return Json(new { success = true, stats });
//            }
//            catch (Exception ex)
//            {
//                // ✅ FIXED: Variable 'ex' is now used
//                return Json(new { success = false, error = ex.Message });
//            }
//        }
//    }

//    // 🧾 QuestPDF Document for Pharmacist Reports
//    public class PharmacistReportDocument : IDocument
//    {
//        private readonly List<Order> _orders;
//        private readonly DateTime _startDate;
//        private readonly DateTime _endDate;
//        private readonly Pharmacist _pharmacist;
//        private readonly string _groupBy;

//        public PharmacistReportDocument(List<Order> orders, DateTime startDate, DateTime endDate, Pharmacist pharmacist, string groupBy)
//        {
//            _orders = orders;
//            _startDate = startDate;
//            _endDate = endDate;
//            _pharmacist = pharmacist;
//            _groupBy = groupBy;
//        }

//        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

//        public void Compose(IDocumentContainer container)
//        {
//            container.Page(page =>
//            {
//                page.Size(PageSizes.A4);
//                page.Margin(30);
//                page.DefaultTextStyle(x => x.FontSize(10));

//                page.Header().Element(ComposeHeader);
//                page.Content().Element(ComposeContent);
//                page.Footer().Element(ComposeFooter);
//            });
//        }

//        private void ComposeHeader(IContainer container)
//        {
//            container.Column(column =>
//            {
//                // Main header
//                column.Item().Row(row =>
//                {
//                    row.RelativeItem().Column(col =>
//                    {
//                        col.Item().Text("Ibhayi Pharmacy - Pharmacist Report")
//                            .FontSize(16).Bold().FontColor(Colors.Blue.Darken3);

//                        col.Item().Text($"Pharmacist: {_pharmacist.ApplicationUser?.Name} {_pharmacist.ApplicationUser?.Surname}")
//                            .FontSize(11).SemiBold();

//                        col.Item().Text($"Period: {_startDate:dd MMMM yyyy} to {_endDate:dd MMMM yyyy}")
//                            .FontSize(10).FontColor(Colors.Grey.Darken1);
//                    });

//                    // ✅ FIXED: Simple placeholder without FitArea()
//                    row.ConstantItem(60).Image(Placeholders.Image);
//                });

//                column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
//            });
//        }

//        private void ComposeContent(IContainer container)
//        {
//            container.PaddingVertical(10).Column(column =>
//            {
//                // Summary section
//                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(summaryCol =>
//                {
//                    summaryCol.Item().Text("REPORT SUMMARY").FontSize(12).Bold();
//                    summaryCol.Item().Row(row =>
//                    {
//                        row.RelativeItem().Text($"Total Orders: {_orders.Count}");
//                        row.RelativeItem().Text($"Total Customers: {_orders.Select(o => o.CustomerID).Distinct().Count()}");
//                    });
//                    summaryCol.Item().Row(row =>
//                    {
//                        var totalItems = _orders.Sum(o => o.OrderLines.Count(ol => ol.Status == "Dispensed"));
//                        var totalValue = _orders.Sum(o => decimal.Parse(o.TotalDue));
//                        row.RelativeItem().Text($"Total Medications Dispensed: {totalItems}");
//                        row.RelativeItem().Text($"Total Value: R {totalValue:F2}");
//                    });
//                });

//                column.Item().PaddingVertical(10);

//                // Group by Customer (as per ONP400 requirements)
//                var customersGroup = _orders
//                    .GroupBy(o => o.Customer)
//                    .OrderBy(g => g.Key?.ApplicationUser?.Surname)
//                    .ThenBy(g => g.Key?.ApplicationUser?.Name);

//                foreach (var customerGroup in customersGroup)
//                {
//                    var customer = customerGroup.Key;
//                    var customerName = customer?.ApplicationUser != null
//                        ? $"{customer.ApplicationUser.Name} {customer.ApplicationUser.Surname}"
//                        : "Unknown Customer";

//                    // Customer header
//                    column.Item().PaddingBottom(5).Background(Colors.Blue.Lighten5).Padding(8).Column(custCol =>
//                    {
//                        custCol.Item().Text($"CUSTOMER: {customerName}").FontSize(12).Bold().FontColor(Colors.Blue.Darken3);
//                        if (customer?.ApplicationUser?.IDNumber != null)
//                        {
//                            custCol.Item().Text($"ID Number: {customer.ApplicationUser.IDNumber}").FontSize(9);
//                        }
//                    });

//                    // Customer's orders
//                    foreach (var order in customerGroup.OrderBy(o => o.OrderDate))
//                    {
//                        var dispensedLines = order.OrderLines.Where(ol => ol.Status == "Dispensed").ToList();

//                        if (!dispensedLines.Any()) continue;

//                        column.Item().PaddingVertical(5).Border(1).BorderColor(Colors.Grey.Lighten2).Padding(8).Column(orderCol =>
//                        {
//                            // Order header
//                            orderCol.Item().Row(row =>
//                            {
//                                row.RelativeItem().Text($"Order: {order.OrderNumber}").SemiBold();
//                                row.ConstantItem(120).AlignRight().Text($"Date: {order.OrderDate:dd MMM yyyy}");
//                            });

//                            // Medications table
//                            orderCol.Item().PaddingTop(5).Table(table =>
//                            {
//                                table.ColumnsDefinition(columns =>
//                                {
//                                    columns.ConstantColumn(25); // No
//                                    columns.RelativeColumn(3);  // Medication
//                                    columns.ConstantColumn(60); // Schedule
//                                    columns.ConstantColumn(50); // Quantity
//                                    columns.ConstantColumn(70); // Unit Price
//                                    columns.ConstantColumn(80); // Line Total
//                                });

//                                // Table header
//                                table.Header(header =>
//                                {
//                                    header.Cell().Text("#").Bold();
//                                    header.Cell().Text("Medication").Bold();
//                                    header.Cell().Text("Schedule").Bold();
//                                    header.Cell().AlignRight().Text("Qty").Bold();
//                                    header.Cell().AlignRight().Text("Unit Price").Bold();
//                                    header.Cell().AlignRight().Text("Line Total").Bold();
//                                });

//                                // Table rows
//                                int lineNumber = 1;
//                                foreach (var line in dispensedLines)
//                                {
//                                    var medication = line.Medications;
//                                    var lineTotal = line.Quantity * line.ItemPrice;

//                                    table.Cell().Text(lineNumber.ToString());
//                                    table.Cell().Text(medication?.MedicationName ?? "Unknown");
//                                    table.Cell().Text(medication?.Schedule ?? "N/A");
//                                    table.Cell().AlignRight().Text(line.Quantity.ToString());
//                                    table.Cell().AlignRight().Text($"R {line.ItemPrice:F2}");
//                                    table.Cell().AlignRight().Text($"R {lineTotal:F2}");

//                                    lineNumber++;
//                                }
//                            });

//                            // Order total
//                            var orderTotal = dispensedLines.Sum(ol => ol.Quantity * ol.ItemPrice);
//                            orderCol.Item().PaddingTop(5).AlignRight()
//                                .Text($"Order Total: R {orderTotal:F2}")
//                                .SemiBold().FontColor(Colors.Green.Darken2);
//                        });
//                    }

//                    // Customer subtotal
//                    var customerSubtotal = customerGroup.Sum(o =>
//                        o.OrderLines.Where(ol => ol.Status == "Dispensed")
//                         .Sum(ol => ol.Quantity * ol.ItemPrice));

//                    column.Item().PaddingTop(5).AlignRight()
//                        .Text($"Customer Subtotal: R {customerSubtotal:F2}")
//                        .Bold().FontSize(11).FontColor(Colors.Blue.Darken2);

//                    column.Item().PaddingBottom(15).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
//                }

//                // Grand total
//                var grandTotal = _orders.Sum(o =>
//                    o.OrderLines.Where(ol => ol.Status == "Dispensed")
//                     .Sum(ol => ol.Quantity * ol.ItemPrice));

//                column.Item().PaddingTop(10).Background(Colors.Green.Lighten5).Padding(10).AlignCenter()
//                    .Text($"GRAND TOTAL: R {grandTotal:F2}")
//                    .Bold().FontSize(14).FontColor(Colors.Green.Darken3);
//            });
//        }

//        private void ComposeFooter(IContainer container)
//        {
//            container.Height(30).Row(row =>
//            {
//                row.RelativeItem().AlignLeft().Text(text =>
//                {
//                    text.Span("Generated on: ").FontColor(Colors.Grey.Medium);
//                    text.Span($"{DateTime.Now:dd MMMM yyyy HH:mm}").SemiBold();
//                });

//                row.RelativeItem().AlignCenter().Text(text =>
//                {
//                    text.CurrentPageNumber();
//                    text.Span(" / ");
//                    text.TotalPages();
//                }). FontColor(Colors.Grey.Medium);

//                row.RelativeItem().AlignRight().Text("Ibhayi Pharmacy - Confidential").FontColor(Colors.Grey.Medium);
//            });
//        }
//    }
//}