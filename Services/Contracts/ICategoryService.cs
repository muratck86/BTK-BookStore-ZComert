using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync(bool trackChanges=false);
        Task<CategoryDto> GetOneCategoryByIdAsync(int id, bool trackChanges=false);
        Task<CategoryDto> CreateOneCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task UpdateCategoryAsnc(int id, CategoryUpdateDto categoryDto, bool trackChanges = false);
        Task DeleteOneCategoryAsync(int id, bool trackChanges=false);

    }
}
