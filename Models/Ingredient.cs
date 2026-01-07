using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace restaurant_prf.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }      // Primary Key

        public string Name { get; set; } = string.Empty;
        [ValidateNever]
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
              // Many ↔ Many
    }
}