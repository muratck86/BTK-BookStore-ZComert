using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EfCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) => 
            await GetAll(trackChanges).OrderBy(b => b.Id).ToListAsync();

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await GetByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public void UpdateOneBook(Book book) => Update(book);
    }
}
