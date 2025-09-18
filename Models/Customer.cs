using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string IdNumber { get; set; }

        [Required]
        public string Cellphone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Customer Allergies
        public ICollection<CustomerAllergy> CustomerAllergies { get; set; }

        // Link to ASP.NET Identity User
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // --- Prescriptions uploaded by Customer ---
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
