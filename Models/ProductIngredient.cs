namespace restaurant_prf.Models
{
    public class ProductIngredient
    {
        public int ProductId { get; set; }          // FK
        public Product Product { get; set; } = null!;

        public int IngredientId { get; set; }       // FK
        public Ingredient Ingredient { get; set; } = null!;
    }
}