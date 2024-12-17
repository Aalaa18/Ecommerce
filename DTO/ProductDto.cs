namespace Ecommerce.DTO
{
    public class ProductDto
    {
         public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
    }
}
