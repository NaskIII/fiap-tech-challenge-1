using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ClientDtos.Request
{
    public class ClientIdentificationRequest
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(14, ErrorMessage = "O campo {0} deve ter exatamente {1} caracteres.", MinimumLength = 14)]
        public string Cpf { get; set; } = default!;
    }
}
