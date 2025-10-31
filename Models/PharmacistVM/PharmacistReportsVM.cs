// Models/PharmacistVM/PharmacistReportsVM.cs
using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class PharmacistReportsVM
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-30);

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Group By")]
        public string GroupBy { get; set; } = "Patient"; // Changed to string

        [Display(Name = "Include Schedule Information")]
        public bool IncludeSchedule { get; set; } = true;

        [Display(Name = "Include Pricing Information")]
        public bool IncludePricing { get; set; } = true;

        // Results properties
        public List<DispensingReportItem> ReportItems { get; set; } = new List<DispensingReportItem>();
        public ReportSummary Summary { get; set; } = new ReportSummary();
        public bool ReportGenerated { get; set; }
    }

    // Removed enum - using string instead

    public class DispensingReportItem
    {
        // Common properties
        public string GroupKey { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string GroupType { get; set; } = string.Empty;

        // Medication details
        public string MedicationName { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public string DosageForm { get; set; } = string.Empty;

        // Patient details (when grouped by patient)
        public string PatientName { get; set; } = string.Empty;
        public string PatientIDNumber { get; set; } = string.Empty;

        // Dispensing details
        public int QuantityDispensed { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime DispensingDate { get; set; }
        public string PharmacistName { get; set; } = string.Empty;

        // Doctor information
        public string DoctorName { get; set; } = string.Empty;
    }

    public class ReportSummary
    {
        public int TotalMedicationsDispensed { get; set; }
        public int TotalPatientsServed { get; set; }
        public int TotalUniqueMedications { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, int> ScheduleBreakdown { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> MedicationBreakdown { get; set; } = new Dictionary<string, int>();
    }
}