using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }
        [ForeignKey("PharmacistID")]
        public int PharmacistID { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public string TotalDue { get; set; }

        public int VAT { get; set; }
    }
}
