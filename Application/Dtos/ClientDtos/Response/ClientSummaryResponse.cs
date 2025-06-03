namespace Application.Dtos.Client.Response
{
    public class ClientSummaryResponse
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; } = default!;
        public string Cpf { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
