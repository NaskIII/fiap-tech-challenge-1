using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDtos.Request
{
    public class AuthenticateUserRequest
    {

        [Required]
        [EmailAddress(ErrorMessage = "Endereço de E-mail Inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        [StringLength(100, ErrorMessage = "A senha deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
