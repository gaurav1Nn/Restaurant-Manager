using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurant_prf.Models;
using restaurant_prf.Models.TequilasRestaurant.Models;

namespace restaurant_prf.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ----- MANY TO MANY CONFIGURATION -----
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // ----- CATEGORY SEED DATA -----
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Appetizer" },
                new Category { CategoryId = 2, Name = "Italian" },
                new Category { CategoryId = 3, Name = "Side Dish" },
                new Category { CategoryId = 4, Name = "Dessert" },
                new Category { CategoryId = 5, Name = "Beverages" }
            );

            // ----- INGREDIENT SEED DATA -----
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Tomato" },
                new Ingredient { IngredientId = 2, Name = "Onion" },
                new Ingredient { IngredientId = 3, Name = "Garlic" },
                new Ingredient { IngredientId = 4, Name = "Cheese" },
                new Ingredient { IngredientId = 5, Name = "Olive" },
                new Ingredient { IngredientId = 6, Name = "Capsicum" }
            );

            // ----- PRODUCT SEED DATA -----
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Taco",
                    Description = "A delicious taco",
                    Price = 2.50m,
                    Stock = 100,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Tomato Taco",
                    Description = "A delicious tomato taco",
                    Price = 1.99m,
                    Stock = 100,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Cheese Taco",
                    Description = "A cheesy taco with melted cheese",
                    Price = 2.49m,
                    Stock = 80,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Onion Taco",
                    Description = "A flavorful taco with onions",
                    Price = 3.49m,
                    Stock = 60,
                    CategoryId = 2
                }
            );

            // ----- PRODUCT INGREDIENT SEED DATA -----
            modelBuilder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 1 },
                new ProductIngredient { ProductId = 1, IngredientId = 4 },
                new ProductIngredient { ProductId = 1, IngredientId = 5 },
                new ProductIngredient { ProductId = 1, IngredientId = 6 },

                new ProductIngredient { ProductId = 2, IngredientId = 2 },
                new ProductIngredient { ProductId = 2, IngredientId = 4 },
                new ProductIngredient { ProductId = 2, IngredientId = 5 },
                new ProductIngredient { ProductId = 2, IngredientId = 6 },

                new ProductIngredient { ProductId = 3, IngredientId = 3 },
                new ProductIngredient { ProductId = 3, IngredientId = 4 },
                new ProductIngredient { ProductId = 3, IngredientId = 5 },
                new ProductIngredient { ProductId = 3, IngredientId = 6 }
            );
        }
    }
}
