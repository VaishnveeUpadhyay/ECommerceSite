using ECommerceSite.Infrastructure.IRepository;
using ECommerceSite.Infrastructure.IService;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;
using ECommerceSite.Repository;
using ECommerceSite.Service;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerceSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService<Product> _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService<Product> productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <returns>Ok response with Product data</returns>
        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            _logger.LogInformation("GetProductById method get called, Response- " + product);
            return product != null ? Ok(product) : NoContent();

        }

        /// <summary>
        /// Get Product by Category Name
        /// </summary>
        /// <returns>Ok response with Product data</returns>
        [HttpGet]
        [Route("GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            var product = await _productService.GetProductByCategory(category);
            _logger.LogInformation("GetProductByCategory method get called, Response- " + product);
            return product != null ? Ok(product) : NoContent();

        }

        /// <summary>
        /// Get Product by Name
        /// </summary>
        /// <returns>Ok response with Product data</returns>
        [HttpGet]
        [Route("GetProductByName")]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var product = await _productService.GetProductByName(productName);
            _logger.LogInformation("GetProductByName method get called, Response- " + product);
            return product != null ? Ok(product) : NoContent();

        }

        /// <summary>
        /// Get Product list
        /// </summary>
        /// <returns>Ok response with Product list in case of success</returns>
        [HttpGet]
        [Route("GetProductList")]
        public async Task<IActionResult> GetAllProducts()
        {
            var productList = await _productService.GetAllProducts();
            _logger.LogInformation("GetAllProducts method get called, Response- " + productList);
            return productList != null? Ok(productList) : NoContent();
        }

        /// <summary>
        ///Add Product
        /// </summary>
        /// <returns>Created response in case of success</returns>
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest productRequest)
        {
            Product product = new();
            if (productRequest != null)
            {
                product.Name = productRequest.Name;
                product.Price = productRequest.Price;
                product.Rating = productRequest.Rating;
                product.CategoryId = productRequest.CategoryId;

                var result = await _productService.AddProduct(product);
                _logger.LogInformation("AddProduct method get called, Response- " + result);
                return Created(result, HttpStatusCode.Created);
            }
            return BadRequest();
        }

        /// <summary>
        ///Update Product
        /// </summary>
        /// <returns>Created response in case of success</returns>
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([Required] int id, [FromBody] UpdateProductRequest product)
        {
            var updated = await _productService.UpdateProduct(id, product);
            _logger.LogInformation("UpdateProduct method get called, Response- " + updated);
            return updated ? Created("Product Data has Updated Successfully", HttpStatusCode.Created) : BadRequest();

        }

        /// <summary>
        ///Delete Product
        /// </summary>
        /// <returns>Ok response in case of success</returns>
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProduct(id);
            _logger.LogInformation("DeleteProduct method get called, Response- " + deleted);
            return deleted ? Ok("Record has been deleted successfully") : NoContent();
        }

    }
}
