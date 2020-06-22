using System.ComponentModel.DataAnnotations;

namespace ItauIti.Challenge.Api.Dtos.Requests
{
    public class PasswordValidateRequestDto
    {
        [Required]
        public string Password { get; set; }
    }
}
