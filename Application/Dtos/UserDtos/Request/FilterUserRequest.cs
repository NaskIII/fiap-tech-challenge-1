using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDtos.Request
{
    public class FilterUserRequest
    {
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "Endereço de E-mail Inválido.")]
        public string? Email { get; set; }
    }
}
