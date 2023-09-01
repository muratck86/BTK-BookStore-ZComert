using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record CategoryUpdateDto : CategoryManipulateDto
    {
        [Required]
        public int Id { get; init; }
    }
}
