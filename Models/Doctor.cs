using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int PracticeNo { get; set; }

        [Required]
        public string CellphoneNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
