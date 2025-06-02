using Domain.Enums;

namespace Domain.Entities
{
    public class Payment
    {

        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentStatus PaymentStatus { get; set; }
        public string? TransactionId { get; set; }
        public string? QrCodeUri { get; set; }
    }
}
