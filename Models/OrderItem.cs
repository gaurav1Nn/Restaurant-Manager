using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace restaurant_prf.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }   // Primary Key

        public int OrderId { get; set; }       // Foreign Key
        public Order? Order { get; set; }      // Navigation Property

        public int ProductId { get; set; }     // Foreign Key
        public Product? Product { get; set; }  // Navigation Property

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
