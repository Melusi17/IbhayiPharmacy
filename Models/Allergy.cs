using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models
{
    public class Allergy
    {
        [Key]
        public int AllergyId { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerAllergy> CustomerAllergies { get; set; }
    }
}
