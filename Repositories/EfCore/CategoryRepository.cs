using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EfCore
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateOneCategory(Category category) => Create(category);

        public void DeleteOneCategory(Category category) => Delete(category);

        public void UpdateOneCategory(Category category) => Update(category);
    }
}
