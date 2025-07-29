using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class Customer
    {
        [Key]
        public int CustormerID { get; set; }

        [ForeignKey("UserID")]
        public int UserId { get; set; }

        [Required]
        public string Allergy { get; set; }
    }
}
