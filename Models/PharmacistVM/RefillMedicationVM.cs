using System;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class RefillMedicationVM
    {
        public int ScriptLineID { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public string MedicationName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Instructions { get; set; } = string.Empty;
        public int TotalRepeats { get; set; }
        public int RepeatsLeft { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public DateTime LastRefillDate { get; set; }
        public int CurrentPrice { get; set; }
        public string Schedule { get; set; } = string.Empty;
    }
}