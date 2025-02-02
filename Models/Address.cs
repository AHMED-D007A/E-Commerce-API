using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public bool IsPrimary { get; set; }

        public string AdditionalInfo { get; set; } = string.Empty;

        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }
    }
}
