using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace IbhayiPharmacy.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [Required]
        public DateTime DateIssued { get; set; }

        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }

        [ForeignKey("PharmacistID")]
        public int PharmacistID { get; set; }

        
        public byte Script { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
    }
}
