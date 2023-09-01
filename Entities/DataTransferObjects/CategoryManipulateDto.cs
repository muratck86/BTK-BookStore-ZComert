using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record CategoryManipulateDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public String? Name { get; init; }
    }
}
