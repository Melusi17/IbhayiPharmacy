using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class OrderLine
    {
        [Key]
        public int OrderLineID { get; set; }
        [ForeignKey("OrderID")]
        public int OrderID { get; set; }
        [ForeignKey("ScriptLineID")]
        public int ScriptLineID { get; set; }
        [Required]
        public int ItemPrice { get; set; }
        [ForeignKey("MedicationID")]
        public int MedicationID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
