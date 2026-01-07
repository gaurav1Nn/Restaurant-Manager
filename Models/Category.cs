namespace restaurant_prf.Models
{
    using System.Collections.Generic;

    namespace TequilasRestaurant.Models
    {
        public class Category
        {
            public int CategoryId { get; set; }        // Primary Key

            public string Name { get; set; } = string.Empty;

            public ICollection<Product> Products { get; set; }
                            // 1 → Many
        }
    }

}