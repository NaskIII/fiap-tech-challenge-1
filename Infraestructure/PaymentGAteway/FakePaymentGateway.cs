using Domain.BaseInterfaces;

namespace Infraestructure.PaymentGateway
{
    public class FakePaymentGateway : IPaymentGateway
    {
        public Task<Guid> ProcessPaymentAsync(decimal amount, string paymentMethod)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}
