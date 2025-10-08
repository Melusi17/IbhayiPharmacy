using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace IbhayiPharmacy.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }   // PK

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // ---- Uploaded Script (BLOB) ----
        [Required]
        public DateTime DateIssued { get; set; }=System.DateTime.Now;
        [ValidateNever]
        public byte[] Script { get; set; }
        [ForeignKey("DoctorID")]
        public int? DoctorID { get; set; }
        [ValidateNever]
        public Doctor Doctors { get; set; }

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