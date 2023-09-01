using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;

namespace Repositories.EfCore
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<PagedList<Author>> GetAllAuthorsAsync(AuthorParameters authorParameters, bool trackChanges)
        {
            var list = await GetAll(trackChanges)
                .Search(authorParameters.SearchTerm)
                .SortBy(authorParameters.OrderBy)
                .ToListAsync();

            var pagedList = PagedList<Author>.ToPagedList(
                list, authorParameters.PageNumber, authorParameters.PageSize);
            return pagedList;
        }

        public async Task<Author> GetOneAuthorByIdAsync(int id, bool trackChanges)
        {
            return await GetByCondition(a => a.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateOneAuthor(Author author) => Create(author);

        public void DeleteOneAuthor(Author author) => Delete(author);

        public void UpdateOneAuthor(Author author) => Update(author);
    }
}
