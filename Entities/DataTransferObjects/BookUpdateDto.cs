using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record BookUpdateDto : BookManipulationDto
    {
        [Required]
        public int Id { get; init; }
    }

}
