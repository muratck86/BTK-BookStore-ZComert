using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record AuthorManipulationDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "First Name too short, length must be between 2 and 50 characters.")]
        [MaxLength(50, ErrorMessage = "First Name too long, length must be between 2 and 50 characters.")]
        public String FirstName { get; init; }

        [Required]
        [MinLength(2, ErrorMessage = "Last Name too short, length must be between 2 and 50 characters.")]
        [MaxLength(50, ErrorMessage = "Last Name too long, length must be between 2 and 50 characters.")]
        public String LastName { get; init; }

        [MinLength(2, ErrorMessage = "Country too short, length must be between 2 and 50 characters.")]
        [MaxLength(50, ErrorMessage = "Country too long, length must be between 2 and 50 characters.")]
        public String Country { get; init; }

        public DateTime BirthDate { get; init; }

    }

    public record AuthorDto
    {
        public int Id { get; init; }
        public String? FirstName { get; init; }
        public String? LastName { get; init; }
        public DateTime? BirthDate { get; init; }
        public String? Country { get; init; }
    }
}
