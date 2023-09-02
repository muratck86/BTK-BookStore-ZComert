using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record BookManipulationDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title too short, length must be between 2 and 50 characters.")]
        [MaxLength(50, ErrorMessage = "Title too long, length must be between 2 and 50 characters.")]
        public string Title { get; init; }

        [Required]
        [Range(10,10000, ErrorMessage = "Price must be between 10 and 10000")]
        public decimal Price { get; init; }

        public int PageCount { get; set; }

        public int Edition { get; set; }

        public int ReleaseYear { get; set; }
    }
}
