using IbhayiPharmacy.Data;
using IbhayiPharmacy.PharmacistVM;
using IbhayiPharmacy.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IbhayiPharmacy.Controllers
{
    [Authorize(Roles = "Pharmacist")]
    public class PharmacistReportController : Controller
    {
        private readonly PharmacistReportService _reportService;
        private readonly ApplicationDbContext _context;

        public PharmacistReportController(PharmacistReportService reportService, ApplicationDbContext context)
        {
            _reportService = reportService;
            _context = context;
        }

        // GET: Display the report form (empty)
        public IActionResult Index()
        {
            var model = new PharmacistReportVM();
            return View(model);
        }

        // POST: Show preview in the same view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PharmacistReportVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validate date range
            if (!model.IsValidDateRange())
            {
                ModelState.AddModelError("EndDate", "End Date must be after Start Date");
                return View(model);
            }

            try
            {
                // Get the current pharmacist ID
                var pharmacistId = await GetCurrentPharmacistId();
                if (pharmacistId == 0)
                {
                    TempData["Error"] = "Pharmacist not found. Please ensure you are logged in correctly.";
                    return View(model);
                }

                // Get pharmacist name for the report header
                var pharmacistName = await GetCurrentPharmacistName();

                // Get the report data
                var reportData = await _reportService.GetDispensedMedicationsAsync(
                    model.StartDate,
                    model.EndDate,
                    pharmacistId,
                    model.GroupBy
                );

                // Pass data to view for preview
                ViewBag.ReportData = reportData;
                ViewBag.PharmacistName = pharmacistName;
                ViewBag.ShowPreview = true;

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while generating the preview: {ex.Message}";
                return View(model);
            }
        }

        // POST: Generate PDF (separate action)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePDF(PharmacistReportVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // Validate date range
            if (!model.IsValidDateRange())
            {
                ModelState.AddModelError("EndDate", "End Date must be after Start Date");
                return View("Index", model);
            }

            try
            {
                // Get the current pharmacist ID
                var pharmacistId = await GetCurrentPharmacistId();
                if (pharmacistId == 0)
                {
                    TempData["Error"] = "Pharmacist not found. Please ensure you are logged in correctly.";
                    return View("Index", model);
                }

                // Get pharmacist name for the report header
                var pharmacistName = await GetCurrentPharmacistName();

                // Get the report data
                var reportData = await _reportService.GetDispensedMedicationsAsync(
                    model.StartDate,
                    model.EndDate,
                    pharmacistId,
                    model.GroupBy
                );

                // Generate PDF
                var pdfBytes = _reportService.GeneratePDF(
                    reportData,
                    model.GroupBy,
                    model.StartDate,
                    model.EndDate,
                    pharmacistName
                );

                // Return PDF file
                var fileName = $"DispensedPrescriptions_{model.StartDate:yyyyMMdd}_{model.EndDate:yyyyMMdd}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while generating the PDF: {ex.Message}";
                return View("Index", model);
            }
        }

        // Helper method to get current pharmacist ID
        private async Task<int> GetCurrentPharmacistId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return 0;

            var pharmacist = await _context.Pharmacists
                .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            return pharmacist?.PharmacistID ?? 0;
        }

        // Helper method to get current pharmacist name
        private async Task<string> GetCurrentPharmacistName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return "Unknown Pharmacist";

            var pharmacist = await _context.Pharmacists
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);

            if (pharmacist?.ApplicationUser != null)
            {
                return $"{pharmacist.ApplicationUser.Name} {pharmacist.ApplicationUser.Surname}";
            }

            return "Unknown Pharmacist";
        }
    }
}