using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Book { Id = 2, Title = "Semerkant", Price = 105 },
                new Book { Id = 3, Title = "Simyacı", Price = 90 }
                );
        }
    }
}
