using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync(bool trackChanges = false)
        {
            var categories = await _manager.Category.GetAllCategoriesAsync(trackChanges);
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return categoryDtos;
        }

        public async Task<CategoryDto> GetOneCategoryByIdAsync(int id, bool trackChanges = false)
        {
            var category =  await GetOneCategoryHelper(id, trackChanges);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsnc(int id, CategoryUpdateDto categoryDto, bool trackChanges = false)
        {
            var category = await GetOneCategoryHelper(id, trackChanges);
            _mapper.Map(categoryDto, category);
            _manager.Category.UpdateOneCategory(category);

            await _manager.SaveAsync();
        }

        public async Task<CategoryDto> CreateOneCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            _manager.Category.CreateOneCategory(category);
            await _manager.SaveAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteOneCategoryAsync(int id, bool trackChanges = false)
        {
            var category = await GetOneCategoryHelper(id, trackChanges);
            _manager.Category.Delete(category);
            await _manager.SaveAsync();
        }

        private async Task<Category> GetOneCategoryHelper(int id, bool trackChanges)
        {
            return await _manager.Category.GetCategoryByIdAsync(id, trackChanges)
                ?? throw new CategoryNotFoundException(id);
        }
    }
}
