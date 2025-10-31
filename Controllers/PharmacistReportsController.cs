// Controllers/PharmacistReportsController.cs
using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Policy = "Pharmacist")]
    public class PharmacistReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmacistReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Main reports dashboard
        public IActionResult Index()
        {
            var viewModel = new PharmacistReportsVM();
            return View(viewModel);
        }

        // POST: Generate dispensing report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateDispensingReport(PharmacistReportsVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                // Validate date range
                if (model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError("", "Start date cannot be after end date.");
                    return View("Index", model);
                }

                if ((model.EndDate - model.StartDate).Days > 365)
                {
                    ModelState.AddModelError("", "Date range cannot exceed 1 year.");
                    return View("Index", model);
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pharmacist = await _context.Pharmacists
                    .Include(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

                if (pharmacist == null)
                {
                    TempData["ErrorMessage"] = "Pharmacist not found.";
                    return View("Index", model);
                }

                // Get dispensed orders for the pharmacist within date range
                var dispensedData = await GetDispensedData(model.StartDate, model.EndDate, pharmacist.PharmacistID);

                // Generate report based on grouping
                model.ReportItems = GenerateReportItems(dispensedData, model.GroupBy);
                model.Summary = GenerateReportSummary(dispensedData);
                model.ReportGenerated = true;

                TempData["SuccessMessage"] = $"Report generated successfully! Found {model.Summary.TotalMedicationsDispensed} medications dispensed.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error generating report: {ex.Message}";
            }

            return View("Index", model);
        }

        // GET: Generate PDF report
        public async Task<IActionResult> GeneratePdfReport(DateTime startDate, DateTime endDate, string groupBy, bool includeSchedule = true, bool includePricing = true)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var pharmacist = await _context.Pharmacists
                    .Include(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(p => p.ApplicationUserId == currentUserId);

                if (pharmacist == null)
                {
                    TempData["ErrorMessage"] = "Pharmacist not found.";
                    return RedirectToAction(nameof(Index));
                }

                var dispensedData = await GetDispensedData(startDate, endDate, pharmacist.PharmacistID);
                var reportItems = GenerateReportItems(dispensedData, groupBy);
                var summary = GenerateReportSummary(dispensedData);

                // For now, redirect back with success message since PDF generation is complex
                // You can implement PDF generation later with iTextSharp or QuestPDF

                TempData["SuccessMessage"] = "PDF report functionality coming soon! For now, use the web view.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error generating PDF report: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<List<DispensingReportItem>> GetDispensedData(DateTime startDate, DateTime endDate, int pharmacistId)
        {
            var data = await _context.OrderLines
                .Where(ol => ol.Status == "Dispensed" &&
                           ol.Order != null &&
                           ol.Order.OrderDate >= startDate &&
                           ol.Order.OrderDate <= endDate &&
                           ol.Order.PharmacistID == pharmacistId)
                .Include(ol => ol.Order)
                    .ThenInclude(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                .Include(ol => ol.Order)
                    .ThenInclude(o => o.Pharmacist)
                        .ThenInclude(p => p.ApplicationUser)
                .Include(ol => ol.Order)
                    .ThenInclude(o => o.Doctor)
                .Include(ol => ol.Medications)
                    .ThenInclude(m => m.DosageForm)
                .Select(ol => new DispensingReportItem
                {
                    MedicationName = ol.Medications != null ? ol.Medications.MedicationName : "Unknown",
                    Schedule = ol.Medications != null ? ol.Medications.Schedule : "Unknown",
                    DosageForm = ol.Medications != null && ol.Medications.DosageForm != null ?
                        ol.Medications.DosageForm.DosageFormName : "Unknown",
                    PatientName = ol.Order != null && ol.Order.Customer != null && ol.Order.Customer.ApplicationUser != null ?
                        $"{ol.Order.Customer.ApplicationUser.Name} {ol.Order.Customer.ApplicationUser.Surname}" : "Unknown",
                    PatientIDNumber = ol.Order != null && ol.Order.Customer != null && ol.Order.Customer.ApplicationUser != null ?
                        ol.Order.Customer.ApplicationUser.IDNumber : "Unknown",
                    QuantityDispensed = ol.Quantity,
                    UnitPrice = ol.Medications != null ? ol.Medications.CurrentPrice : 0,
                    LineTotal = ol.Medications != null ? ol.Medications.CurrentPrice * ol.Quantity : 0,
                    DispensingDate = ol.Order != null ? ol.Order.OrderDate : DateTime.MinValue,
                    PharmacistName = ol.Order != null && ol.Order.Pharmacist != null && ol.Order.Pharmacist.ApplicationUser != null ?
                        $"{ol.Order.Pharmacist.ApplicationUser.Name} {ol.Order.Pharmacist.ApplicationUser.Surname}" : "Unknown",
                    DoctorName = ol.Order != null && ol.Order.Doctor != null ?
                        $"Dr. {ol.Order.Doctor.Name} {ol.Order.Doctor.Surname}" : "Not Specified"
                })
                .ToListAsync();

            return data ?? new List<DispensingReportItem>();
        }

        private List<DispensingReportItem> GenerateReportItems(List<DispensingReportItem> data, string groupBy)
        {
            if (data == null || !data.Any())
                return new List<DispensingReportItem>();

            return groupBy?.ToLower() switch
            {
                "medication" => data
                    .OrderBy(d => d.MedicationName)
                    .ThenBy(d => d.PatientName)
                    .ToList(),

                "schedule" => data
                    .OrderBy(d => d.Schedule)
                    .ThenBy(d => d.MedicationName)
                    .ToList(),

                _ => data // Default to patient grouping
                    .OrderBy(d => d.PatientName)
                    .ThenBy(d => d.DispensingDate)
                    .ToList()
            };
        }

        private ReportSummary GenerateReportSummary(List<DispensingReportItem> data)
        {
            var summary = new ReportSummary();

            if (data != null && data.Any())
            {
                summary.TotalMedicationsDispensed = data.Sum(d => d.QuantityDispensed);
                summary.TotalPatientsServed = data.Select(d => d.PatientIDNumber).Distinct().Count();
                summary.TotalUniqueMedications = data.Select(d => d.MedicationName).Distinct().Count();
                summary.TotalRevenue = data.Sum(d => d.LineTotal);

                // Schedule breakdown
                summary.ScheduleBreakdown = data
                    .GroupBy(d => d.Schedule)
                    .ToDictionary(g => $"Schedule {g.Key}", g => g.Sum(x => x.QuantityDispensed));

                // Medication breakdown (top 10)
                summary.MedicationBreakdown = data
                    .GroupBy(d => d.MedicationName)
                    .OrderByDescending(g => g.Sum(x => x.QuantityDispensed))
                    .Take(10)
                    .ToDictionary(g => g.Key, g => g.Sum(x => x.QuantityDispensed));
            }

            return summary;
        }
    }
}