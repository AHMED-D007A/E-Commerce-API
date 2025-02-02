namespace E_Commerce_API.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public int ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
