using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid clientId, string cpf);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
