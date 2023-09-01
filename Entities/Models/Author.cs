namespace Entities.Models
{
    public class Author
    {
        public int Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public String? Country { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
