using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models.PharmacyManagerVM
{
    
    public class StockOrderVM
    {
 

        public virtual StockOrder StockOrder { get; set; }
        public virtual List<StockOrderDetail> OrderLines { get; set; } = new List<StockOrderDetail>();

        public virtual Medication Medication { get; set; }

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
