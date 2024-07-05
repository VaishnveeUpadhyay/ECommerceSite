using ECommerceSite.Infrastructure.IRepository;
using ECommerceSite.Infrastructure.IService;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;

namespace ECommerceSite.Service
{
    public class ProductService : IProductService<Product>
    {
        private readonly IProductRepository<Product> _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository<Product> productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Product?> GetProductById(int id)
        {
            _logger.LogInformation("Returned the result of GetProductyById() in Service class");
            return await _productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            _logger.LogInformation("Returned the result of GetProductByCategory() in Service class");
            return await _productRepository.GetProductByCategory(category);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            _logger.LogInformation("Returned the result of GetProductByName() in Service class");
            return await _productRepository.GetProductByName(productName);
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            _logger.LogInformation("Returned the result of GetAllProducts() in Service class");
            return await _productRepository.GetAllProducts();
        }

        public async Task<string> AddProduct(Product product)
        {
            _logger.LogInformation("Returned the result of AddProduct() in Service class");
            return await _productRepository.AddProduct(product);
        }

        public async Task<bool> UpdateProduct(int id, UpdateProductRequest product)
        {
            _logger.LogInformation("Returned the result of UpdateProduct() in Service class");
            return await _productRepository.UpdateProduct(id, product);
        }
        public async Task<bool> DeleteProduct(int id)
        {
            _logger.LogInformation("Returned the result of DeleteProduct() in Service class");
            return await _productRepository.DeleteProduct(id);
        }

    }
}
