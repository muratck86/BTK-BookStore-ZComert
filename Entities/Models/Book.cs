
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

        //Ref: navigation property
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
