using Domain.Enums;

namespace Domain.BaseInterfaces
{
    public interface INotification
    {
        public Task NotifyKitchen(OrderStatus status);
        public Task NotifyClient(OrderStatus status);
    }
}
