using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class CustomerAllergy
    {
        
        [Key]
        public int Customer_AllergyId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

        public int Active_IngredientID { get; set; }
        


    }
}
