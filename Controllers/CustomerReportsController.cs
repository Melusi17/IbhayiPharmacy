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
            Console.WriteLine($"Error previewing report: {ex.Message}");
            return PartialView("_ReportPreview", new PrescriptionReportVM());
        }
    }






    private async Task<List<PrescriptionItemVM>> GetDispensedPrescriptionsData(int customerId, DateTime startDate, DateTime endDate)
    {
        var reportData = new List<PrescriptionItemVM>();

        // Get the current customer's ApplicationUserId
        var customer = await _context.Customers
            .Where(c => c.CustormerID == customerId)
            .Select(c => new { c.ApplicationUserId })
            .FirstOrDefaultAsync();

        if (customer == null || string.IsNullOrEmpty(customer.ApplicationUserId))
            return reportData;

        var endOfDay = endDate.Date.AddDays(1).AddSeconds(-1);

        // 1. Get data from Orders (Dispensed orders) for this customer
        var orderData = await _context.Orders
            .Include(o => o.Doctor)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Medications)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ScriptLine)
            .Where(o => o.CustomerID == customerId &&
                       o.OrderDate >= startDate &&
                       o.OrderDate <= endOfDay &&
                       o.Status.ToLower() == "dispensed")
            .ToListAsync();

        foreach (var order in orderData)
        {
            foreach (var orderLine in order.OrderLines.Where(ol => ol.Medications != null))
            {
                reportData.Add(new PrescriptionItemVM
                {
                    Date = order.OrderDate,
                    Medication = orderLine.Medications.MedicationName,
                    Quantity = orderLine.Quantity,
                    Repeats = orderLine.ScriptLine != null ? orderLine.ScriptLine.Repeats : 0,
                    DoctorName = order.Doctor != null ?
                        $"{order.Doctor.Name} {order.Doctor.Surname}" : "Unknown Doctor",
                    Instructions = orderLine.ScriptLine != null ? orderLine.ScriptLine.Instructions : string.Empty
                });
            }
        }

        // 2. Get data from Prescriptions for this customer's ApplicationUserId
        var prescriptionData = await _context.Prescriptions
            .Include(p => p.Doctors)
            .Include(p => p.scriptLines)
                .ThenInclude(sl => sl.Medications)
            .Where(p => p.ApplicationUserId == customer.ApplicationUserId &&
                       p.DateIssued >= startDate &&
                       p.DateIssued <= endOfDay &&
                       (p.Status.ToLower() == "dispensed" || p.Status.ToLower() == "approved"))
            .ToListAsync();

        foreach (var prescription in prescriptionData)
        {
            foreach (var scriptLine in prescription.scriptLines.Where(sl => sl.Medications != null))
            {
                reportData.Add(new PrescriptionItemVM
                {
                    Date = prescription.DateIssued,
                    Medication = scriptLine.Medications.MedicationName,
                    Quantity = scriptLine.Quantity,
                    Repeats = scriptLine.Repeats,
                    DoctorName = prescription.Doctors != null ?
                        $"{prescription.Doctors.Name} {prescription.Doctors.Surname}" : "Unknown Doctor",
                    Instructions = scriptLine.Instructions
                });
            }
        }

        // 3. Get data from ScriptLines for this customer's ApplicationUserId
        var scriptLineData = await _context.ScriptLines
            .Include(sl => sl.Medications)
            .Include(sl => sl.Prescriptions)
                .ThenInclude(p => p.Doctors)
            .Where(sl => sl.Prescriptions.ApplicationUserId == customer.ApplicationUserId &&
                        (sl.Status.ToLower() == "approved" || sl.Status.ToLower() == "dispensed") &&
                        sl.ApprovedDate >= startDate &&
                        sl.ApprovedDate <= endOfDay)
            .ToListAsync();

        foreach (var scriptLine in scriptLineData.Where(sl => sl.Medications != null))
        {
            reportData.Add(new PrescriptionItemVM
            {
                Date = scriptLine.ApprovedDate ?? scriptLine.Prescriptions.DateIssued,
                Medication = scriptLine.Medications.MedicationName,
                Quantity = scriptLine.Quantity,
                Repeats = scriptLine.Repeats,
                DoctorName = scriptLine.Prescriptions.Doctors != null ?
                    $"{scriptLine.Prescriptions.Doctors.Name} {scriptLine.Prescriptions.Doctors.Surname}" : "Unknown Doctor",
                Instructions = scriptLine.Instructions
            });
        }

        // Remove potential duplicates by grouping on key fields
        return reportData
            .GroupBy(x => new {
                Date = x.Date.Date,
                x.Medication,
                x.DoctorName,
                x.Quantity,
                x.Repeats
            })
            .Select(g => g.First())
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Medication)
            .ToList();
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



}