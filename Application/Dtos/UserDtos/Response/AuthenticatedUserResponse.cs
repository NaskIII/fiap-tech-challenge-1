namespace Application.Dtos.UserDtos.Response
{
    public class AuthenticatedUserResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string Roles { get; set; } = "Administrador";
    }
}
