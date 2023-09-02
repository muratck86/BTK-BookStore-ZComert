using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repositories.EfCore.Config
{
    public class CategoryConfig: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            builder.HasData(
                new Category { Id = 1, Name = "Mystery"},
                new Category { Id = 2, Name = "Romance" },
                new Category { Id = 3, Name = "Fiction" },
                new Category { Id = 4, Name = "Horror" },
                new Category { Id = 5, Name = "Memoir" },
                new Category { Id = 6, Name = "Biography" },
                new Category { Id = 7, Name = "Fantasy" },
                new Category { Id = 8, Name = "Poetry" },
                new Category { Id = 9, Name = "Thriller" },
                new Category { Id = 10, Name = "Humor" },
                new Category { Id = 11, Name = "Science-Fiction" },
                new Category { Id = 12, Name = "Self-Help" },
                new Category { Id = 13, Name = "Short Stories" }
                );
        }
    }
}
