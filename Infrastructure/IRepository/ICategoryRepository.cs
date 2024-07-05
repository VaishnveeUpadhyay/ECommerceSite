using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;

namespace ECommerceSite.Infrastructure.IRepository
{
    public interface ICategoryRepository<T> where T : class
    {
        Task<Category?> GetCategoryById(int id);

        Task<IEnumerable<Category>> GetAllCategories();

        Task<string> AddCategory(Category category);

        Task<bool> UpdateCategory(int id, UpdateCategoryRequest category);

        Task<bool> DeleteCategory(int id);

    }
}
