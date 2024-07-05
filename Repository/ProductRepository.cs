using ECommerceSite.Controllers;
using ECommerceSite.Database;
using ECommerceSite.Infrastructure.IRepository;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSite.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ECommerceDbContext dbContext, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Product?> GetProductById(int id)
        {
            _logger.LogInformation("Call gets to the database in repository layer and return the product searched by Id");
            return await _dbContext.Product.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            _logger.LogInformation("Call gets to the database in repository layer and return the products searched by category name");
            return await _dbContext.Product
            .Include(p => p.Category)
            .Where(p => p.Category.Name == categoryName)
            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            _logger.LogInformation("Call gets to the database in repository layer and return the products searched by product name");
            return await _dbContext.Product.Where(p => p.Name == productName).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            _logger.LogInformation("Call gets to the database in repository layer, return the list of existing products");
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<string> AddProduct(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Record has been added successfully");
            return "Record has been added successfully";
        }

        public async Task<bool> UpdateProduct(int id, UpdateProductRequest requestUpdate)
        {
            Product? product = await _dbContext.Product.FindAsync(id);

            if (product != null)
            {
                product.Name = requestUpdate.Name;
                product.Price = requestUpdate.Price;
                product.Rating = requestUpdate.Rating;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Record has been added successfully");
                return true;
            }
            _logger.LogInformation("Record does not exist, returned False");
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            Product? productToBeDeleted = _dbContext.Product.Find(id);
            if(productToBeDeleted != null)
            {
                _dbContext.Product.Remove(productToBeDeleted);
                await _dbContext.SaveChangesAsync();

                List<Product> listOfAllProducts = await _dbContext.Product.ToListAsync();

                _logger.LogInformation("Record has been deleted successfully, returned True");
                return true;
            }
            _logger.LogInformation("Record does not exist, returned False");
            return false;
        }

    }
}
