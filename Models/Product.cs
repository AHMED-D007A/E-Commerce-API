using Microsoft.AspNetCore.Server.HttpSys;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;

        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
