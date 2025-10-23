using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models.PharmacistVM;
using System.Security.Claims;

[Authorize(Policy = "Customer")]
public class CustomerReportsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CustomerReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult PrescriptionReport()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate, string groupBy)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Filter orders by ApplicationUserId
        var orders = await _context.Orders
            .Include(o => o.Doctor)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Medications)
            .Include(o => o.Customer)
            .Where(o => o.OrderDate >= startDate
                        && o.OrderDate <= endDate
                        && o.Customer.ApplicationUserId == currentUserId)
            .ToListAsync();

        groupBy = groupBy == "Medication" ? "Medication" : "Doctor";

        List<ReportGroup> groups;

        if (groupBy == "Medication")
        {
            groups = orders
                .SelectMany(o => o.OrderLines)
                .GroupBy(ol => ol.Medications.MedicationName)
                .Select(g => new ReportGroup
                {
                    GroupName = g.Key,
                    Records = g.Select(ol => new PrescriptionItemVM
                    {
                        Medication = ol.Medications.MedicationName,
                        Quantity = ol.Quantity,
                        Repeats = ol.Quantity,
                        Date = ol.Order.OrderDate,
                        DoctorName = ol.Order.Doctor?.Name ?? "Unknown Doctor",
                        Instructions = ol.RejectionReason ?? string.Empty
                    }).ToList(),
                    Subtotal = g.Sum(ol => ol.Quantity)
                }).ToList();
        }
        else
        {
            groups = orders
                .GroupBy(o => o.Doctor?.Name ?? "Unknown Doctor")
                .Select(g => new ReportGroup
                {
                    GroupName = g.Key,
                    Records = g.SelectMany(o => o.OrderLines)
                        .Select(ol => new PrescriptionItemVM
                        {
                            Medication = ol.Medications.MedicationName,
                            Quantity = ol.Quantity,
                            Repeats = ol.Quantity,
                            Date = ol.Order.OrderDate,
                            DoctorName = ol.Order.Doctor?.Name ?? "Unknown Doctor",
                            Instructions = ol.RejectionReason ?? string.Empty
                        }).ToList(),
                    Subtotal = g.SelectMany(o => o.OrderLines).Sum(ol => ol.Quantity)
                }).ToList();
        }

        var report = new PrescriptionReportVM
        {
            StartDate = startDate,
            EndDate = endDate,
            Groups = groups,
            GrandTotal = groups.Sum(g => g.Subtotal),
            GroupBy = groupBy
        };

        var pdfBytes = ReportGenerator.GeneratePrescriptionReport(report);
        return File(pdfBytes, "application/pdf", $"Prescription_Report_{DateTime.Now:yyyyMMdd}.pdf");
    }

    [HttpPost]
    public async Task<IActionResult> PreviewReport(DateTime startDate, DateTime endDate, string groupBy)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var orders = await _context.Orders
            .Include(o => o.Doctor)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Medications)
            .Include(o => o.Customer)
            .Where(o => o.OrderDate >= startDate
                        && o.OrderDate <= endDate
                        && o.Customer.ApplicationUserId == currentUserId)
            .ToListAsync();

        groupBy = groupBy == "Medication" ? "Medication" : "Doctor";

        List<ReportGroup> groups;

        if (groupBy == "Medication")
        {
            groups = orders
                .SelectMany(o => o.OrderLines)
                .GroupBy(ol => ol.Medications.MedicationName)
                .Select(g => new ReportGroup
                {
                    GroupName = g.Key,
                    Records = g.Select(ol => new PrescriptionItemVM
                    {
                        Medication = ol.Medications.MedicationName,
                        Quantity = ol.Quantity,
                        Repeats = ol.Quantity,
                        Date = ol.Order.OrderDate,
                        DoctorName = ol.Order.Doctor?.Name ?? "Unknown Doctor",
                        Instructions = ol.RejectionReason ?? string.Empty
                    }).ToList(),
                    Subtotal = g.Sum(ol => ol.Quantity)
                }).ToList();
        }
        else
        {
            groups = orders
                .GroupBy(o => o.Doctor?.Name ?? "Unknown Doctor")
                .Select(g => new ReportGroup
                {
                    GroupName = g.Key,
                    Records = g.SelectMany(o => o.OrderLines)
                        .Select(ol => new PrescriptionItemVM
                        {
                            Medication = ol.Medications.MedicationName,
                            Quantity = ol.Quantity,
                            Repeats = ol.Quantity,
                            Date = ol.Order.OrderDate,
                            DoctorName = ol.Order.Doctor?.Name ?? "Unknown Doctor",
                            Instructions = ol.RejectionReason ?? string.Empty
                        }).ToList(),
                    Subtotal = g.SelectMany(o => o.OrderLines).Sum(ol => ol.Quantity)
                }).ToList();
        }

        var report = new PrescriptionReportVM
        {
            StartDate = startDate,
            EndDate = endDate,
            Groups = groups,
            GrandTotal = groups.Sum(g => g.Subtotal),
            GroupBy = groupBy
        };

        return PartialView("_ReportPreview", report);
    }
}
