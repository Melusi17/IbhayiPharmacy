using IbhayiPharmacy.Models;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class PrescriptionViewModel
    {
        public Prescription Prescription { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Doctor SelectedDoctor { get; set; }
        public List<Medication> AvailableMedications { get; set; }
        public List<Doctor> AvailableDoctors { get; set; }
        public List<Customer> AvailableCustomers { get; set; }
        public List<ScriptLine> CurrentMedications { get; set; }
    }
}
 