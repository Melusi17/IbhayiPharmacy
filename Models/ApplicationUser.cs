using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models
{
    public class ApplicationUser : IdentityUser
    {
       

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
        [Required]
        public string IDNumber { get; set; }

        public string? CellphoneNumber { get; set; }
    
    }
}
