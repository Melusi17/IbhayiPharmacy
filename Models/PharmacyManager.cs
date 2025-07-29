using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class PharmacyManager
    {
        [Key]
        public int PharmacyManagerID { get; set; }

        [ForeignKey("UserID")]
        public int UserId { get; set; }

        [Required]
        public string HealthCouncilRegNo { get; set; }

    }
}
