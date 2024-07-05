using ECommerceSite.Models.DTOs;
using ECommerceSite.Models;

namespace ECommerceSite.Infrastructure.IService
{
    public interface IProductService<T> where T : class
    {
        Task<Product?> GetProductById(int id);

        Task<IEnumerable<Product>> GetProductByCategory(string category);

        Task<IEnumerable<Product>> GetProductByName(string productName);

        Task<IEnumerable<Product>> GetAllProducts();

        Task<string> AddProduct(Product product);

        Task<bool> UpdateProduct(int id, UpdateProductRequest product);

        Task<bool> DeleteProduct(int id);
    }
}
