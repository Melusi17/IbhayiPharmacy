using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    // View Models
    public class CustomerDashboardVM
    {
        public List<Prescription> UnprocessedPrescriptions { get; set; } /*= new();*/
        public List<Prescription> ProcessedPrescriptions { get; set; } /*= new();*/
        public List<MedicationHistoryVM> MedicationHistory { get; set; }
    }

    public class PlaceOrderVM
    {
        public List<Doctor> Doctors { get; set; } = new();
        public List<Medication> Medications { get; set; } = new();
        public DateTime OrderDate { get; set; }
    }

    public class RepeatMedicationVM
    {
        public int ScriptLineId { get; set; }
        public string MedicationName { get; set; } = string.Empty;
        public int RepeatsLeft { get; set; }
        public DateTime LastRefillDate { get; set; }
        public string Instructions { get; set; } = string.Empty;
    }

    public class OrderSubmissionVM
    {
        public List<OrderItemVM> OrderItems { get; set; } = new();
        public int? DoctorId { get; set; }
        public int? PrescriptionId { get; set; } // ADDED: To track which prescription is being ordered
    }

    public class OrderItemVM
    {
        public int MedicationId { get; set; }
        public int ScriptLineId { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; } = string.Empty;
        public bool IsRepeat { get; set; }
    }

    public class ReportRequestVM
    {
        public DateTime ReportDate { get; set; }
    }

    // NEW: Order Tracking View Model
    public class OrderTrackingVM
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string TotalDue { get; set; } = string.Empty;
        public List<OrderLineVM> OrderLines { get; set; } = new();
    }

    // NEW: Order Line View Model for tracking
    public class OrderLineVM
    {
        public string MedicationName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Instructions { get; set; } = string.Empty;
        public int ItemPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public string? RejectionReason { get; set; }
        public string DoctorName { get; set; } = string.Empty; // For the table display
    }
    
    // Reports View Models
    public class PrescriptionReportVM
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public DateTime GeneratedOn { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Group By")]
        public string GroupBy { get; set; } = "Doctor"; // "Doctor" or "Medication"

        public List<ReportGroup> Groups { get; set; } = new();
        public int GrandTotal { get; set; }
    }

    public class ReportGroup
    {
        public string GroupName { get; set; } = string.Empty;
        public List<PrescriptionItemVM> Records { get; set; } = new();
        public int Subtotal { get; set; }
    }

    public class PrescriptionItemVM
    {
        public DateTime Date { get; set; }
        public string Medication { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Repeats { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;

    }

    public class MedicationHistoryVM
    {
        public int MedicationID { get; set; }
        public string MedicationName { get; set; }
        public string DoctorName { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public int TotalOrders { get; set; }
        public int RepeatsUsed { get; set; }
        public int TotalRepeats { get; set; }
        public int RepeatsLeft { get; set; }
        public bool IsActive { get; set; }
    }
}