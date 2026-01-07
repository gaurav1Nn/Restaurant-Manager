using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using restaurant_prf.Models.TequilasRestaurant.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant_prf.Models
{
    public class Product
    {
        public Product()
        {
            ProductIngredients = new List<ProductIngredient>();
        }
        public int ProductId { get; set; }            // Primary Key

        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile? ImageFile {  get; set; }
        public string ImageUrl {get; set;  } = "https://via.placeholder.com/150";
        // Foreign Key
        [ValidateNever]
        public Category? Category { get; set; }       // Navigation Property
        [ValidateNever]
        public ICollection<OrderItem> OrderItems { get; set; }
        [ValidateNever]// 1 → 
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
                     // Many ↔ Many
    }
}
