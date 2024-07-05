using ECommerceSite.Models.DTOs;
using ECommerceSite.Models;

namespace ECommerceSite.Infrastructure.IService
{
    public interface ICategoryService<T> where T : class
    {
        Task<Category?> GetCategoryById(int id);

        Task<IEnumerable<Category>> GetAllCategories();

        Task<string> AddCategory(Category category);

        Task<bool> UpdateCategory(int id, UpdateCategoryRequest category);

        Task<bool> DeleteCategory(int id);
    }
}
