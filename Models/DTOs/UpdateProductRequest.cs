using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models.DTOs
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        [Range(0,5)]
        public double Rating { get; set; }
        public int CategoryId { get; set; }
    }
}
