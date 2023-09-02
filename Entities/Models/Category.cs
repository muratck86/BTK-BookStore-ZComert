namespace Entities.Models
{
    public class  Category
    {
        public int Id { get; set; }
        public String? Name { get; set; }

        //Ref: Collection Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
