using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace IbhayiPharmacy.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }   // PK

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateIssued { get; set; }   // When the doctor issued the prescription

        // ---- Foreign Keys ----
        [Required]
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }

        [Required]
        [ForeignKey("Pharmacist")]
        public int PharmacistID { get; set; }
        public Pharmacist Pharmacist { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        // ---- Uploaded Script (BLOB) ----
        [Required]
        public DateTime DateIssued { get; set; }
        [ValidateNever]
        public byte[] Script { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }   // Original file name

        [MaxLength(100)]
        public string ContentType { get; set; } // MIME type (PDF, image, etc.)

        // ---- Status for processing ----
        [Required]
        public bool IsProcessed { get; set; } = false;   // false = unprocessed, true = processed
    }
}






//[ForeignKey("ApplicationUserId")]
//public string ApplicationUserId { get; set; }
//[ValidateNever]
//public ApplicationUser ApplicationUser { get; set; }