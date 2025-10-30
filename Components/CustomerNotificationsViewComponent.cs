//using Microsoft.AspNetCore.Mvc;
//using IbhayiPharmacy.Services;
//using IbhayiPharmacy.Models;
//using System.Security.Claims;
//using IbhayiPharmacy.Models.PharmacistVM;

//namespace IbhayiPharmacy.Components
//{
//    public class CustomerNotificationsViewComponent : ViewComponent
//    {
//        private readonly INotificationService _notificationService;

//        public CustomerNotificationsViewComponent(INotificationService notificationService)
//        {
//            _notificationService = notificationService;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            try
//            {
//                // Get the current logged-in user's ID
//                var applicationUserId = UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//                if (string.IsNullOrEmpty(applicationUserId))
//                {
//                    return View(new CustomerNotificationViewModel());
//                }

//                var notifications = await _notificationService.GetCustomerNotificationsAsync(applicationUserId);
//                return View(notifications);
//            }
//            catch (Exception ex)
//            {
//                // Log error and return empty model
//                Console.WriteLine($"Error in CustomerNotifications: {ex.Message}");
//                return View(new CustomerNotificationViewModel());
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using IbhayiPharmacy.Services;
using IbhayiPharmacy.Models;
using System.Security.Claims;
using IbhayiPharmacy.Models.PharmacistVM;

namespace IbhayiPharmacy.Components
{
    public class CustomerNotificationsViewComponent : ViewComponent
    {
        private readonly INotificationService _notificationService;

        public CustomerNotificationsViewComponent(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var applicationUserId = UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(applicationUserId))
                {
                    return View(new CustomerNotificationViewModel());
                }

                var notifications = await _notificationService.GetCustomerNotificationsAsync(applicationUserId);
                return View(notifications);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error in CustomerNotifications: {ex.Message}");
                return View(new CustomerNotificationViewModel());
            }
        }
    }
}