using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IbhayiPharmacy.Models.PharmacyManagerVM
{
    
    public class StockOrderVM
    {
 

        public virtual StockOrder StockOrder { get; set; }
        public virtual List<StockOrderDetail> OrderLines { get; set; } = new List<StockOrderDetail>();

        public virtual Medication Medication { get; set; }

    }
    public class ManagerProfile
    {
        [Key]
        public int ProfileID { get; set; }

        [Required]
        public string UserId { get; set; } // FK to IdentityUser.Id

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string IDNumber { get; set; }

        public string? CellphoneNumber { get; set; }
    }
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
    }

    public class EmailModel
    {
        [Key]
        public int EmailId { get; set; }

        public string SenderEmail { get; set; }
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


    }
}
