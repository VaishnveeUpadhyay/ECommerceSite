using ECommerceSite.Infrastructure.IRepository;
using ECommerceSite.Infrastructure.IService;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;

namespace ECommerceSite.Service
{
    public class CategoryService : ICategoryService<Category>
    {
        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepository<Category> categoryRepository, ILogger<CategoryService> logger) 
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            _logger.LogInformation("Returned the result of GetCategoryById() in Service class");
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            _logger.LogInformation("Returned the result of GetAllCategories() in Service class");
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<string> AddCategory(Category category)
        {
            _logger.LogInformation("Returned the result of AddCategory() in Service class");
            return await _categoryRepository.AddCategory(category);
        }
       
        public async Task<bool> UpdateCategory(int id, UpdateCategoryRequest category)
        {
            _logger.LogInformation("Returned the result of UpdateCategory() in Service class");
            return await _categoryRepository.UpdateCategory(id, category);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            _logger.LogInformation("Returned the result of DeleteCategory() in Service class");
            return await _categoryRepository.DeleteCategory(id);
        }
    }
}
