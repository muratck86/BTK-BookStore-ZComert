using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserForAuthenticaionDto
    {
        [Required]
        public string? UserName { get; init; }

        [Required]
        public string? Password { get; init; }
    }
}
