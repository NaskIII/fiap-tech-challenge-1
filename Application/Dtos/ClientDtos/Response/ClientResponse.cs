using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Client.Response
{
    public class ClientResponse
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string CPF { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public DateTime RegisterDate { get; set; }
    }
}
