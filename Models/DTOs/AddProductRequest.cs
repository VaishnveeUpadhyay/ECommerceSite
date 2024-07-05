using System.ComponentModel.DataAnnotations;

namespace ECommerceSite.Models.DTOs
{
    public class AddProductRequest
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int CategoryId { get; set; }
    }
}
