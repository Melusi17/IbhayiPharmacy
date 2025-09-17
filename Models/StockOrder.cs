using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class StockOrder
    {
        [Key]
        public int StockOrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [ForeignKey("SupplierID")]
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [ForeignKey("MedicationID")]
        public int MedicationID { get; set; }
        public Medication Medication { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
