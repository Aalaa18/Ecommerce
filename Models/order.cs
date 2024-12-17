namespace Ecommerce.Models
{
    public enum PaymentMethods
    {
        chash,
        card
    }
    public class Order
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public PaymentMethods Method { get; set; }
    }
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
