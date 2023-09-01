
namespace Entities.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String? Title { get; set; }
        public decimal Price { get; set; }
        public int PageCount { get; set; }
        public int Edition { get; set; }
        public int ReleaseYear { get; set; }
    }
}
