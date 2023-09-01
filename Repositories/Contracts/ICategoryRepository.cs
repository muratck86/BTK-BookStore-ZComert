using Entities.Models;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetCategoryByIdAsync(int id, bool trackChanges);
        void CreateOneCategory(Category category);

        void DeleteOneCategory(Category category);

        void UpdateOneCategory(Category category);
    }
}
