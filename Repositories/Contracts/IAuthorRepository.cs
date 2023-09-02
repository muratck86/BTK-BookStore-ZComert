using Entities.Models;
using Entities.RequestFeatures;

namespace Repositories.Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author> 
    {
        Task<PagedList<Author>> GetAllAuthorsAsync(AuthorParameters authorParameters, bool trackChanges);
        Task<Author> GetOneAuthorByIdAsync(int id, bool trackChanges);
        void CreateOneAuthor(Author author);
        void DeleteOneAuthor(Author author);
        void UpdateOneAuthor(Author author);
    }
}
