
namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        ICategoryRepository Category { get; }
        Task SaveAsync();
    }
}
