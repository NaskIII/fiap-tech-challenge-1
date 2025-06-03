using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Client.Request
{
    public class RegisterClientRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "O Nome não pode ter mais de 50 caracteres.")]
        public string Name { get; set; } = default!;

        [Required]
        [StringLength(11, ErrorMessage = "O CPF deve ter exatamente 11 caracteres.", MinimumLength = 11)]
        public string Cpf { get; set; } = default!;

        [Required]
        [EmailAddress(ErrorMessage = "O Email deve ser um endereço de email válido.")]
        public string Email { get; set; } = default!;
    }

}
