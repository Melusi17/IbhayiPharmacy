using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class CustomerNotificationViewModel
    {
        [Display(Name = "Available for Ordering")]
        public int AvailableForOrderingCount { get; set; }

        [Display(Name = "Available for Refills")]
        public int AvailableForRefillsCount { get; set; }

        [Display(Name = "Ready for Collection")]
        public int ReadyForCollectionCount { get; set; }

        [Display(Name = "Rejected Medications")]
        public int RejectedMedicationsCount { get; set; }

        [Display(Name = "Pending Orders")]
        public int PendingOrdersCount { get; set; }
    }
}
