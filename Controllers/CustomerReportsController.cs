using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models.PharmacistVM;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

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
        var model = new PrescriptionReportVM
        {
            StartDate = DateTime.Now.AddMonths(-1),
            EndDate = DateTime.Now
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerateReport(PrescriptionReportVM model)
    {
        if (!ModelState.IsValid)
        {
            return View("PrescriptionReport", model);
        }

        try
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentUserId))
            {
                TempData["Error"] = "User not authenticated. Please log in again.";
                return RedirectToAction("PrescriptionReport");
            }

            // Get the current customer based on the logged-in user's ID
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.ApplicationUserId == currentUserId);

            if (customer == null)
            {
                TempData["Error"] = "Customer profile not found.";
                return RedirectToAction("PrescriptionReport");
            }

            // Get dispensed prescriptions and orders for the current customer
            var reportData = await GetDispensedPrescriptionsData(customer.CustormerID, model.StartDate, model.EndDate);

            var groups = GenerateReportGroups(reportData, model.GroupBy);

            var report = new PrescriptionReportVM
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Groups = groups,
                GrandTotal = groups.Sum(g => g.Subtotal),
                GroupBy = model.GroupBy
            };

            var pdfBytes = ReportGenerator.GeneratePrescriptionReport(report);
            return File(pdfBytes, "application/pdf",
                $"My_Prescription_Report_{model.StartDate:yyyyMMdd}_to_{model.EndDate:yyyyMMdd}.pdf");
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Error generating report: {ex.Message}");
            TempData["Error"] = "An error occurred while generating the report. Please try again.";
            return RedirectToAction("PrescriptionReport");
        }
    }

    [HttpPost]
    public async Task<IActionResult> PreviewReport(DateTime startDate, DateTime endDate, string groupBy)
    {
        try
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return PartialView("_ReportPreview", new PrescriptionReportVM());
            }

            // Get the current customer based on the logged-in user's ID
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.ApplicationUserId == currentUserId);

            if (customer == null)
            {
                return PartialView("_ReportPreview", new PrescriptionReportVM());
            }

            // Get dispensed prescriptions and orders for the current customer
            var reportData = await GetDispensedPrescriptionsData(customer.CustormerID, startDate, endDate);

            var groups = GenerateReportGroups(reportData, groupBy);

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
        catch (Exception ex)
        {
            // Log exception
            Console.WriteLine($"Error previewing report: {ex.Message}");
            return PartialView("_ReportPreview", new PrescriptionReportVM());
        }
    }

    private async Task<List<PrescriptionItemVM>> GetDispensedPrescriptionsData(int customerId, DateTime startDate, DateTime endDate)
    {
        var reportData = new List<PrescriptionItemVM>();

        // OPTION 1: Get data from Orders (Dispensed orders)
        var dispensedOrders = await _context.Orders
            .Include(o => o.Doctor)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Medications)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ScriptLine)
            .Where(o => o.CustomerID == customerId &&
                       o.OrderDate >= startDate &&
                       o.OrderDate <= endDate &&
                       o.Status.ToLower() == "dispensed")
            .ToListAsync();

        foreach (var order in dispensedOrders)
        {
            foreach (var orderLine in order.OrderLines)
            {
                if (orderLine.Medications != null)
                {
                    reportData.Add(new PrescriptionItemVM
                    {
                        Date = order.OrderDate,
                        Medication = orderLine.Medications.MedicationName,
                        Quantity = orderLine.Quantity,
                        Repeats = orderLine.ScriptLine?.Repeats ?? 0,
                        DoctorName = order.Doctor != null
                            ? $"{order.Doctor.Name} {order.Doctor.Surname}"
                            : "Unknown Doctor",
                        Instructions = orderLine.ScriptLine?.Instructions ?? string.Empty
                    });
                }
            }
        }

        // OPTION 2: Also get data from Prescriptions if they have a dispensed status
        var dispensedPrescriptions = await _context.Prescriptions
            .Include(p => p.Doctors)
            .Include(p => p.scriptLines)
                .ThenInclude(sl => sl.Medications)
            .Where(p => p.ApplicationUserId == _context.Customers
                .Where(c => c.CustormerID == customerId)
                .Select(c => c.ApplicationUserId)
                .FirstOrDefault() &&
                       p.DateIssued >= startDate &&
                       p.DateIssued <= endDate &&
                       (p.Status.ToLower() == "dispensed" || p.Status.ToLower() == "approved"))
            .ToListAsync();

        foreach (var prescription in dispensedPrescriptions)
        {
            foreach (var scriptLine in prescription.scriptLines.Where(sl => sl.Medications != null))
            {
                reportData.Add(new PrescriptionItemVM
                {
                    Date = prescription.DateIssued,
                    Medication = scriptLine.Medications.MedicationName,
                    Quantity = scriptLine.Quantity,
                    Repeats = scriptLine.Repeats,
                    DoctorName = prescription.Doctors != null
                        ? $"{prescription.Doctors.Name} {prescription.Doctors.Surname}"
                        : "Unknown Doctor",
                    Instructions = scriptLine.Instructions
                });
            }
        }

        // OPTION 3: Get from ScriptLines with approved status
        var approvedScriptLines = await _context.ScriptLines
            .Include(sl => sl.Medications)
            .Include(sl => sl.Prescriptions)
                .ThenInclude(p => p.Doctors)
            .Where(sl => sl.Prescriptions.ApplicationUserId == _context.Customers
                .Where(c => c.CustormerID == customerId)
                .Select(c => c.ApplicationUserId)
                .FirstOrDefault() &&
                       (sl.Status.ToLower() == "approved" || sl.Status.ToLower() == "dispensed") &&
                       sl.ApprovedDate >= startDate &&
                       sl.ApprovedDate <= endDate)
            .ToListAsync();

        foreach (var scriptLine in approvedScriptLines)
        {
            if (scriptLine.Medications != null)
            {
                reportData.Add(new PrescriptionItemVM
                {
                    Date = scriptLine.ApprovedDate ?? scriptLine.Prescriptions.DateIssued,
                    Medication = scriptLine.Medications.MedicationName,
                    Quantity = scriptLine.Quantity,
                    Repeats = scriptLine.Repeats,
                    DoctorName = scriptLine.Prescriptions.Doctors != null
                        ? $"{scriptLine.Prescriptions.Doctors.Name} {scriptLine.Prescriptions.Doctors.Surname}"
                        : "Unknown Doctor",
                    Instructions = scriptLine.Instructions
                });
            }
        }

        return reportData;
    }

    private List<ReportGroup> GenerateReportGroups(List<PrescriptionItemVM> reportData, string groupBy)
    {
        groupBy = groupBy == "Medication" ? "Medication" : "Doctor";
        List<ReportGroup> groups;

        if (groupBy == "Medication")
        {
            groups = reportData
                .GroupBy(item => item.Medication)
                .Select(g => new ReportGroup
                {
                    GroupName = g.Key,
                    Records = g.OrderBy(r => r.Date).ToList(),
                    Subtotal = g.Sum(item => item.Quantity)
                })
                .Where(g => g.Records.Any())
                .OrderBy(g => g.GroupName)
                .ToList();
        }
        else
        {
            groups = reportData
                .GroupBy(item => item.DoctorName)
                .Select(g => new ReportGroup
                {
                    GroupName = g.Key,
                    Records = g.OrderBy(r => r.Date).ToList(),
                    Subtotal = g.Sum(item => item.Quantity)
                })
                .Where(g => g.Records.Any())
                .OrderBy(g => g.GroupName)
                .ToList();
        }

        return groups;
    }

    [HttpGet]
    public async Task<IActionResult> DebugData()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.ApplicationUserId == currentUserId);

        if (customer == null)
        {
            return Content("Customer not found");
        }

        var debugInfo = $"Customer ID: {customer.CustormerID}<br/>";

        // Check orders
        var orders = await _context.Orders
            .Where(o => o.CustomerID == customer.CustormerID)
            .ToListAsync();
        debugInfo += $"Total Orders: {orders.Count}<br/>";

        foreach (var order in orders)
        {
            debugInfo += $"Order: {order.OrderID}, Date: {order.OrderDate}, Status: {order.Status}<br/>";
        }

        // Check prescriptions
        var prescriptions = await _context.Prescriptions
            .Where(p => p.ApplicationUserId == currentUserId)
            .ToListAsync();
        debugInfo += $"Total Prescriptions: {prescriptions.Count}<br/>";

        return Content(debugInfo);
    }
}