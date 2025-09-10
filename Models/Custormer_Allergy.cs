using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbhayiPharmacy.Models
{
    public class Custormer_Allergy
    {
        [Key]
        public int Custormer_AllergyID { get; set; }

        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }

        [ForeignKey("Active_IngredientID")]
        public int Active_IngredientID { get; set; }
       
    }
}
