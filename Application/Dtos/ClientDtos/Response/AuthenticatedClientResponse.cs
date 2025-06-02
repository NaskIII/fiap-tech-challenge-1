namespace Application.Dtos.ClientDtos.Response
{
    public class AuthenticatedClientResponse
    {

        public Guid ClientId { get; set; }
        public string Name { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
