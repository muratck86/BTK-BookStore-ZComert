using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repositories.EfCore.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();

            builder.HasData(
                new Author { Id = 1, FirstName = "Paulo", LastName = "Coelho", BirthDate = new DateTime(1947,8,24), Country = "Brazil"},
                new Author { Id = 2, FirstName = "Amin", LastName = "Maalouf", BirthDate = new DateTime(1949, 2, 25), Country = "France" },
                new Author { Id = 3, FirstName = "Yaşar", LastName = "Kemal", BirthDate = new DateTime(1923, 10, 6), Country = "Turkey" },
                new Author { Id = 4, FirstName = "Ahmet", LastName = "Ümit", BirthDate = new DateTime(1960, 9, 30), Country = "Turkey" }
                );
        }
    }
}
