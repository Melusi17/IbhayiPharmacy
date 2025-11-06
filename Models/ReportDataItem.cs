namespace IbhayiPharmacy.Models
{
    // This class NEVER touches the database
    // It's not in your DbContext
    // Entity Framework knows nothing about it
    public class ReportDataItem
    {
        public DateTime Date { get; set; }
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public string PatientName { get; set; }
        public string Schedule { get; set; }
    }
}
