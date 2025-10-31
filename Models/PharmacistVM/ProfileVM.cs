using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class ProfileVM
    {
        // Basic Information
        public string Name { get; set; }

        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Cellphone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Cellphone number must be exactly 10 digits")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Please enter a valid 10-digit cellphone number starting with 0")]
        [Display(Name = "Cellphone Number")]
        public string CellphoneNumber { get; set; }

        // Password Change
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }

        // Allergies
        public List<int> SelectedAllergyIds { get; set; } = new List<int>();
        public List<SelectListItem> AllergyList { get; set; } = new List<SelectListItem>();

        // Additional profile info (display only)
        public string IDNumber { get; set; }
        public string CustomerSince { get; set; } = "Recently";
    }
}
