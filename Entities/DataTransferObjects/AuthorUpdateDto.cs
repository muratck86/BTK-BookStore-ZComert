using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record AuthorUpdateDto : AuthorManipulationDto
    {
        [Required]
        public int Id { get; init; }
    }
}
