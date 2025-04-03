namespace KTM_ASP.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public string? Discount {  get; set; }
    }
}
