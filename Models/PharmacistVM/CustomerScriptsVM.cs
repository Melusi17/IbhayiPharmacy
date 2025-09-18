using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class CustomerScriptsVM
    {
        [ValidateNever]
        public IEnumerable<Prescription> ScriptList { get; set; }
        [ValidateNever]
        public Prescription prescription { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public int Prescr { get; set; }
        [ValidateNever]
        public string Name { get; set; }

        [ValidateNever]
        public string Surname { get; set; }
        [ValidateNever]
        public string IDNumber { get; set; }


        //[ValidateNever]
        //public IEnumerable<ScriptLine> ScriptLineList { get; set; }

        ////public int Quantity { get; set; }
        //[ValidateNever]
        //public PresScriptLine PresScriptLine { get; set; }
        //public string Instructions { get; set; }
        //public int ScriptLineID { get; set; }

        //public int Repeats { get; set; }


        //public int RepeatsLeft { get; set; }

    }
}