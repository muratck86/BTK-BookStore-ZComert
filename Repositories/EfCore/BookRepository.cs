using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EfCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context) { }

        public async Task<PagedList<Book>> GetAllBooksAsync(
            BookParameters bookParameters,
            bool trackChanges)
                    {
            var list = await GetAll(trackChanges)
                .FilterByPrice(bookParameters.MinPrice, bookParameters.MaxPrice)
                .Search(bookParameters.SearchTerm)
                .OrderBy(b => b.Id)
                .ToListAsync();

            var pagedList = PagedList<Book>.ToPagedList(
                list, 
                bookParameters.PageNumber, 
                bookParameters.PageSize);
            return pagedList;
        }

        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await GetByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public void UpdateOneBook(Book book) => Update(book);
    }
}
