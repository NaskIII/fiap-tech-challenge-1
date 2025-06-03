namespace Domain.BaseInterfaces
{
    public interface IPaymentGateway
    {
        Task<Guid> ProcessPaymentAsync(decimal amount, string paymentMethod);
    }
}
