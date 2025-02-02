using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        public ICollection<ProductCategory> ProductCategories { get; set;} = new List<ProductCategory>();
    }
}
