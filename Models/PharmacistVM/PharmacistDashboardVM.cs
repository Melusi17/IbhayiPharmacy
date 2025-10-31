using IbhayiPharmacy.Models;
using System.Collections.Generic;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class PharmacistDashboardVM
    {
        public List<Order> PendingOrders { get; set; } = new List<Order>();
        public List<Order> ReadyForCollection { get; set; } = new List<Order>();
        public List<Prescription> UnprocessedPrescriptions { get; set; } = new List<Prescription>();
        public List<Medication> LowStockMedications { get; set; } = new List<Medication>();

        public int TotalPendingOrders { get; set; }
        public int TotalReadyForCollection { get; set; }
        public int TotalUnprocessedScripts { get; set; }
        public int TotalLowStockItems { get; set; }
        public int TodayProcessedOrders { get; set; }
        public int TodayProcessedScripts { get; set; }
    }
}