using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceSite.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        [Range(0,5)]
        public double Rating { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        [JsonIgnore]
        // Navigation property
        public Category? Category { get; set; }
    }
}
