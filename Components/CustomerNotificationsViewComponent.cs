using IbhayiPharmacy.Models.PharmacistVM;
using IbhayiPharmacy.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            // Get the current logged-in user's ID
            var customerId = UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(customerId))
            {
                return View(new CustomerNotificationViewModel());
            }

            var notifications = await _notificationService.GetCustomerNotificationsAsync(customerId);
            return View(notifications);
        }


    }
}
