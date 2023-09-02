using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record BookCreateDto : BookManipulationDto 
    {
        [Required]
        public int CategoryId { get; init; }

        [Required]
        public int AuthorId { get; init; }
    }
}
