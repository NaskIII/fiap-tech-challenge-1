using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ClientDtos.Request
{
    public class ClientIdentificationRequest
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(11, ErrorMessage = "O campo {0} deve ter exatamente {1} caracteres.", MinimumLength = 11)]
        public string Cpf { get; set; } = default!;
    }
}
