using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Dtos.Customer
{
    public class CustomerLoginDto
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
