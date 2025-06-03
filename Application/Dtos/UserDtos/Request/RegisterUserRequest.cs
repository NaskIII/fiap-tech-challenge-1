using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos.Request
{
    public class RegisterUserRequest
    {

        [Required]
        [StringLength(50, ErrorMessage = "O Nome não pode ter mais de 50 caracteres.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "O Email deve ser um endereço de email válido.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "A Senha deve ter no mínimo 6 e no máximo 100 caracteres.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
