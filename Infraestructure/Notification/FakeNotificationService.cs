using Domain.BaseInterfaces;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Notification
{
    public class FakeNotificationService : INotification
    {

        private readonly ILogger _logger;

        public FakeNotificationService(ILogger<FakeNotificationService> logger)
        {
            _logger = logger;
        }

        public Task NotifyClient(OrderStatus status)
        {
            _logger.LogInformation("Fake notification sent to client with status: {Status}", status);
            return Task.CompletedTask;
        }

        public Task NotifyKitchen(OrderStatus status)
        {
            _logger.LogInformation("Fake notification sent to kitchen with status: {Status}", status);
            return Task.CompletedTask;
        }
    }
}
