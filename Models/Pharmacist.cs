using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class Pharmacist
    {
        [Key]
        public int PharmacistID { get; set; }

        [Required]
        public string HealthCouncilRegNo { get; set; }

        // Link to ASP.NET Identity User
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // --- Prescriptions handled by Pharmacist ---
        public ICollection<Prescription> Prescriptions { get; set; }


    }
}
