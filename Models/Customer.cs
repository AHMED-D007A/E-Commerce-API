using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class Customer : User
    {
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
