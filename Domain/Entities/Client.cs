namespace Domain.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    }
}
