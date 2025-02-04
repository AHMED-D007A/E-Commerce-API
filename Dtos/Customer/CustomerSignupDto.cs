using E_Commerce_API.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Dtos.Customer
{
    public class CustomerSignupDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
