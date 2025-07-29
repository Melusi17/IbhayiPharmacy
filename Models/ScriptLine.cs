using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class ScriptLine
    {
        [Key]
        public int ScriptLineID { get; set; }

        [ForeignKey("PrescriptionID")]
        public int PrescriptionID { get; set; }

        [ForeignKey("MedicationID")]
        public int MedicationID { get; set; }

        [ForeignKey("PharmacistID")]
        public int PharmacistID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Instructions { get; set; }

        [Required]
        public int Repeats { get; set; }

        [Required]
        public int RepeatsLeft { get; set; }

    }
}
