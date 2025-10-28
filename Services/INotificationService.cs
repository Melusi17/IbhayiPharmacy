using IbhayiPharmacy.Models.PharmacistVM;

namespace IbhayiPharmacy.Services
{
    public interface INotificationService
    {
        Task<CustomerNotificationViewModel> GetCustomerNotificationsAsync(string customerId);
    }
}
