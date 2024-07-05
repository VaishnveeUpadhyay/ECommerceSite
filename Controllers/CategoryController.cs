using ECommerceSite.Infrastructure.IService;
using ECommerceSite.Models;
using ECommerceSite.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerceSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService<Category> _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService<Category> categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Get Category by Id
        /// </summary>
        /// <returns>Ok response with Category data</returns>
        [HttpGet]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            _logger.LogInformation("GetCategoryById method get called, Response- " + category);
            return category != null ? Ok(category) : NoContent();

        }

        /// <summary>
        /// Get Category List
        /// </summary>
        /// <returns>Ok response with List of Categories in case of success</returns>
        [HttpGet]
        [Route("GetCategoryList")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            _logger.LogInformation("GetAllCategories method get called, Response- " + categories);
            return categories != null ? Ok(categories) : NoContent();
        }

        /// <summary>
        /// Add Category 
        /// </summary>
        /// <returns>Created response in case of success</returns>
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest addCategoryRequest)
        {
            Category category = new();
            if(!String.IsNullOrEmpty(addCategoryRequest.Name))
            {
                category.Name = addCategoryRequest.Name;
                var result = await _categoryService.AddCategory(category);
                _logger.LogInformation("AddCategory method get called, Response- " + result);
                return Created(result, HttpStatusCode.Created);
            }
            return BadRequest();
            
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <returns>Created response in case of success</returns>
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([Required] int id, [FromBody] UpdateCategoryRequest category)
        {
            var updated = await _categoryService.UpdateCategory(id, category);
            _logger.LogInformation("UpdateCategory method get called, Response- " + updated);
            return updated ? Created("Category Data has Updated Successfully", HttpStatusCode.Created) : BadRequest();
        }

        /// <summary>
        /// Delete Category by Id
        /// </summary>
        /// <returns>Ok response in case of success</returns>
        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategory(id);
            _logger.LogInformation("DeleteCategory method get called, Response- " + deleted);
            return deleted ? Ok("Record has been deleted successfully") : NoContent();
        }
    }
}
