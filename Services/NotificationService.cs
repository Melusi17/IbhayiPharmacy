using IbhayiPharmacy.Data;
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

        public async Task<CustomerNotificationViewModel> GetCustomerNotificationsAsync(string customerId)
        {
            var notifications = new CustomerNotificationViewModel();

            try
            {
                // Convert customerId to int if your CustomerID is int-based
                // If CustomerID is string, remove this conversion
                if (int.TryParse(customerId, out int customerIdInt))
                {
                    // 1. Medications available for ordering (Active prescriptions with script lines)
                    notifications.AvailableForOrderingCount = await _context.Prescriptions
                        .Where(p => p.ApplicationUserId == customerId &&
                                   p.Status == "Approved" &&
                                   p.scriptLines.Any(sl => sl.Quantity > 0))
                        .SelectMany(p => p.scriptLines)
                        .CountAsync();

                    // 2. Medications available for refills (ScriptLines with RepeatsLeft > 0)
                    notifications.AvailableForRefillsCount = await _context.ScriptLines
                        .Where(sl => sl.Prescriptions.ApplicationUserId == customerId &&
                                    sl.RepeatsLeft > 0 &&
                                    sl.Quantity > 0)
                        .CountAsync();

                    // 3. Medications ready for collection (Orders with status "Ready for Collection")
                    notifications.ReadyForCollectionCount = await _context.Orders
                        .Where(o => o.CustomerID == customerIdInt &&
                                   o.Status == "Ready for Collection")
                        .SelectMany(o => o.OrderLines)
                        .CountAsync(ol => ol.Status == "Approved");

                    // 4. Rejected medications (OrderLines with rejected status)
                    notifications.RejectedMedicationsCount = await _context.OrderLines
                        .Where(ol => ol.Order.CustomerID == customerIdInt &&
                                    ol.Status == "Rejected")
                        .CountAsync();

                    // 5. Pending orders (Orders with pending status)
                    notifications.PendingOrdersCount = await _context.Orders
                        .Where(o => o.CustomerID == customerIdInt &&
                                   (o.Status == "Pending" || o.Status == "Processing"))
                        .CountAsync();
                }
                else
                {
                    // Handle case where customerId is not an integer (use string comparison)
                    notifications.AvailableForOrderingCount = await _context.Prescriptions
                        .Where(p => p.ApplicationUserId == customerId &&
                                   p.Status == "Approved" &&
                                   p.scriptLines.Any(sl => sl.Quantity > 0))
                        .SelectMany(p => p.scriptLines)
                        .CountAsync();

                    // For other counts that require CustomerID (int), they will remain 0
                    // You might need to adjust your database relationships if CustomerID should be string
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use ILogger here)
                Console.WriteLine($"Error retrieving notifications: {ex.Message}");

                // Return default values in case of error
                return new CustomerNotificationViewModel();
            }

            return notifications;
        }
    }
}
