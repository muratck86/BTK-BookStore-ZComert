using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repositories.EfCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("money");
            builder.HasData(
                new Book { Id = 1, CategoryId = 7, AuthorId = 3, Title = "The Legend of Ararat", Price = 105, Edition = 1, PageCount = 355, ReleaseYear = 1945 },
                new Book { Id = 2, CategoryId = 1, AuthorId = 4, Title = "Patasana", Price = 145, Edition = 3, PageCount = 425, ReleaseYear = 2003 },
                new Book { Id = 3, CategoryId = 7, AuthorId = 1, Title = "The Alchemist", Price = 90, Edition = 5, PageCount = 251, ReleaseYear = 1982 },
                new Book { Id = 4, CategoryId = 2, AuthorId = 1, Title = "Veronica Decides to Die", Price = 125, Edition = 3, PageCount = 388, ReleaseYear = 1992 },
                new Book { Id = 5, CategoryId = 6, AuthorId = 2, Title = "Samarkand", Price = 110, Edition = 18, PageCount = 295, ReleaseYear = 1996 }
                );
        }
    }
}
