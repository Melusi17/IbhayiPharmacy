using System;
using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.PharmacistVM
{
    public class PharmacistReportVM
    {
        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-30);

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Grouping option is required")]
        [Display(Name = "Group By")]
        public string GroupBy { get; set; } = "customer";

        // Validation method to ensure EndDate is after StartDate
        public bool IsValidDateRange()
        {
            return EndDate >= StartDate;
        }
    }
}