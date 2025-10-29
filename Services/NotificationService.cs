using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using IbhayiPharmacy.Models.PharmacistVM;
using Microsoft.EntityFrameworkCore;

namespace IbhayiPharmacy.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerNotificationViewModel> GetCustomerNotificationsAsync(string applicationUserId)
        {
            Console.WriteLine($"🔔 [Service] Starting notification retrieval for user: {applicationUserId}");

            var notifications = new CustomerNotificationViewModel();

            try
            {
                // Find the customer using ApplicationUserId
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);

                if (customer == null)
                {
                    Console.WriteLine($"❌ [Service] No customer found for ApplicationUserId: {applicationUserId}");
                    return notifications; // Return empty notifications
                }

                int customerId = customer.CustormerID;
                Console.WriteLine($"🔔 [Service] Found customer ID: {customerId}");

                // 1. MEDICATIONS AVAILABLE FOR ORDERING
                // Approved ScriptLines that haven't been ordered yet
                notifications.AvailableForOrderingCount = await _context.ScriptLines
                    .Where(sl => sl.Prescriptions.ApplicationUserId == applicationUserId &&
                                sl.Status == "Approved" &&
                                sl.Quantity > 0 &&
                                !_context.OrderLines.Any(ol => ol.ScriptLineID == sl.ScriptLineID)) // Not yet ordered
                    .CountAsync();

                Console.WriteLine($"🔔 [Service] Available for ordering: {notifications.AvailableForOrderingCount}");

                // 2. MEDICATIONS AVAILABLE FOR REFILLS
                // ScriptLines with repeats left (only count if they have repeats available)
                notifications.AvailableForRefillsCount = await _context.ScriptLines
                    .Where(sl => sl.Prescriptions.ApplicationUserId == applicationUserId &&
                                sl.RepeatsLeft > 0 &&
                                sl.Status == "Approved" &&
                                sl.Quantity > 0)
                    .CountAsync();

                Console.WriteLine($"🔔 [Service] Available for refills: {notifications.AvailableForRefillsCount}");

                // 3. READY FOR COLLECTION
                // Orders with ready status and their order lines
                var readyOrders = await _context.Orders
                    .Where(o => o.CustomerID == customerId &&
                               (o.Status == "Ready" || o.Status == "Ready for Collection" || o.Status == "Completed"))
                    .ToListAsync();

                notifications.ReadyForCollectionCount = readyOrders
                    .SelectMany(o => o.OrderLines)
                    .Where(ol => ol.Status == "Approved" || ol.Status == "Completed")
                    .Count();

                Console.WriteLine($"🔔 [Service] Ready for collection: {notifications.ReadyForCollectionCount}");

                // 4. REJECTED MEDICATIONS
                // Count both rejected ScriptLines and OrderLines
                var rejectedScriptLines = await _context.ScriptLines
                    .Where(sl => sl.Prescriptions.ApplicationUserId == applicationUserId &&
                                sl.Status == "Rejected")
                    .CountAsync();

                var rejectedOrderLines = await _context.OrderLines
                    .Where(ol => ol.Order.CustomerID == customerId &&
                                ol.Status == "Rejected")
                    .CountAsync();

                notifications.RejectedMedicationsCount = rejectedScriptLines + rejectedOrderLines;
                Console.WriteLine($"🔔 [Service] Rejected medications: {notifications.RejectedMedicationsCount}");

                // 5. PENDING ORDERS
                // Orders that are being processed (count orders, not order lines)
                notifications.PendingOrdersCount = await _context.Orders
                    .Where(o => o.CustomerID == customerId &&
                               (o.Status == "Ordered" || o.Status == "Pending" || o.Status == "Processing"))
                    .CountAsync();

                Console.WriteLine($"🔔 [Service] Pending orders: {notifications.PendingOrdersCount}");

                // Calculate total active notifications for debugging
                var totalActive = notifications.AvailableForOrderingCount +
                                 notifications.AvailableForRefillsCount +
                                 notifications.ReadyForCollectionCount +
                                 notifications.RejectedMedicationsCount;

                Console.WriteLine($"✅ [Service] Total active notifications: {totalActive}");
                Console.WriteLine($"✅ [Service] All queries completed successfully");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 [Service] ERROR: {ex.Message}");
                Console.WriteLine($"💥 [Service] StackTrace: {ex.StackTrace}");

                // Return zeros instead of test data for production
                notifications.AvailableForOrderingCount = 0;
                notifications.AvailableForRefillsCount = 0;
                notifications.ReadyForCollectionCount = 0;
                notifications.RejectedMedicationsCount = 0;
                notifications.PendingOrdersCount = 0;
            }

            return notifications;
        }
    }
}