using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        public  string? FirstName { get; init; }

        public string? Lastname { get; init; }

        [Required]
        public string? UserName { get; init; }

        [Required]
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }
    }
}
