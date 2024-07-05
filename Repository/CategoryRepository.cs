
using ECommerceSite.Controllers;
using ECommerceSite.Database;
using ECommerceSite.Infrastructure.IRepository;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata;

namespace ECommerceSite.Repository
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(ECommerceDbContext dbContext, ILogger<CategoryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            _logger.LogInformation("Call gets to the database in repository layer and return the category searched by Id");
            return await _dbContext.Category.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            _logger.LogInformation("Call gets to the database in repository layer, return the list of existing categories");
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<string> AddCategory(Category category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Record has been added successfully");
            return "Record has been added successfully";
        }

        public async Task<bool> UpdateCategory(int id, UpdateCategoryRequest requestUpdate)
        {
            Category? category = await _dbContext.Category.FindAsync(id);

            if (category != null)
            {
                category.Name = requestUpdate.Name;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Record has been updated successfully, returned True");
                return true;
            }

            _logger.LogInformation("Record does not exist, returned False");
            return false;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            Category? categoryToBeDeleted = await _dbContext.Category.FindAsync(id);
            if (categoryToBeDeleted != null)
            {
                _dbContext.Category.Remove(categoryToBeDeleted);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Record has been deleted successfully, returned True");
                return true;
            }
            _logger.LogInformation("Record does not exist, returned False");
            return false;
        }
    }
}
