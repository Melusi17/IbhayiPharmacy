using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IbhayiPharmacy.Models.PharmacistVM
{
    public class PharmacistDispensingDashboardVM
    {
        public List<Order> PendingOrders { get; set; } = new List<Order>();
        public List<Order> ReadyForCollection { get; set; } = new List<Order>();
        public List<Order> WaitingCustomerAction { get; set; } = new List<Order>();
        public int TodayDispensed { get; set; }
    }

    public class DispenseOrderVM
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerIDNumber { get; set; } = string.Empty;
        public List<string> CustomerAllergies { get; set; } = new List<string>();
        public string CurrentStatus { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int VAT { get; set; }
        public List<DispenseOrderLineVM> OrderLines { get; set; } = new List<DispenseOrderLineVM>();

        // New properties for processing state
        public bool AllItemsProcessed { get; set; }
        public bool AnyItemsDispensed { get; set; }
        public bool AllItemsRejected { get; set; }

        // For selected medications to dispense
        public List<int> SelectedOrderLineIds { get; set; } = new List<int>();
    }

    public class DispenseOrderLineVM
    {
        public int OrderLineID { get; set; }
        public int MedicationID { get; set; }
        public string MedicationName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
        public decimal LineTotal { get; set; }
        public string Instructions { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public bool IsLowStock { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool CanDispense { get; set; }
        public string? RejectionReason { get; set; }

        // New properties for selection
        public bool IsSelected { get; set; }
        public bool CanBeSelected => Status == "Pending";
    }

    public class DispensingHistoryVM
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalProcessed { get; set; }
        public int ReadyForCollectionCount { get; set; }
        public int WaitingActionCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class RejectOrderLineVM
    {
        public int OrderLineID { get; set; }
        public string MedicationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rejection reason is required")]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string RejectionReason { get; set; } = string.Empty;
    }

    public class CompleteOrderProcessingVM
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int DispensedCount { get; set; }
        public int RejectedCount { get; set; }
        public int PendingCount { get; set; }
        public bool CanComplete { get; set; }
        public string ExpectedStatus { get; set; } = string.Empty;
    }
}