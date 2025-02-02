namespace E_Commerce_API.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public required Category Category {  get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
