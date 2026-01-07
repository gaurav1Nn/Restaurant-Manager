//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using restaurant_prf.Data;
//using restaurant_prf.Models;
//using restaurant_prf.Models.OrderItemViewModels;
//using TequilasRestaurant.Models;


//namespace TequliasRestaurant.Controllers
//{
//    public class OrderController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private Repository<Product> _products;
//        private Repository<Order> _orders;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//            _products = new Repository<Product>(context);
//            _orders = new Repository<Order>(context);
//        }

//        [Authorize]
//        [HttpGet]
//        public async Task<IActionResult> Create()
//        {
//            //ViewBag.Products = await _products.GetAllAsync();

//            //Retrieve or create an OrderViewModel from session or other state management
//            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
//            {
//                OrderItems = new List<OrderItemViewModel>(),
//                Products = await _products.GetAllAsync()
//            };


//            return View(model);
//        }

//        [HttpPost]
//        [Authorize]
//        public async Task<IActionResult> AddItem(int prodId, int prodQty)
//        {
//            var product = await _context.Products.FindAsync(prodId);
//            if (product == null)
//            {
//                return NotFound();
//            }

//            // Retrieve or create an OrderViewModel from session or other state management
//            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
//            {
//                OrderItems = new List<OrderItemViewModel>(),
//                Products = await _products.GetAllAsync()
//            };

//            // Check if the product is already in the order
//            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

//            // If the product is already in the order, update the quantity
//            if (existingItem != null)
//            {
//                existingItem.Quantity += prodQty;
//            }
//            else
//            {
//                model.OrderItems.Add(new OrderItemViewModel
//                {
//                    ProductId = product.ProductId,
//                    Price = product.Price,
//                    Quantity = prodQty,
//                    ProductName = product.Name
//                });
//            }

//            // Update the total amount
//            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

//            // Save updated OrderViewModel to session
//            HttpContext.Session.Set("OrderViewModel", model);

//            // Redirect back to Create to show updated order items
//            return RedirectToAction("Create", model);
//        }

//        [HttpGet]
//        [Authorize]
//        public async Task<IActionResult> Cart()
//        {

//            // Retrieve the OrderViewModel from session or other state management
//            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

//            if (model == null || model.OrderItems.Count == 0)
//            {
//                return RedirectToAction("Create");
//            }

//            return View(model);
//        }

//        [HttpPost]
//        [Authorize]
//        public async Task<IActionResult> PlaceOrder()
//        {
//            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");
//            if (model == null || model.OrderItems.Count == 0)
//            {
//                return RedirectToAction("Create");
//            }

//            // Create a new Order entity
//            Order order = new Order
//            {
//                OrderDate = DateTime.Now,
//                TotalAmount = model.TotalAmount,
//                UserId = _userManager.GetUserId(User)
//            };

//            // Add OrderItems to the Order entity
//            foreach (var item in model.OrderItems)
//            {
//                order.OrderItems.Add(new OrderItem
//                {
//                    ProductId = item.ProductId,
//                    Quantity = item.Quantity,
//                    Price = item.Price
//                });
//            }

//            // Save the Order entity to the database
//            await _orders.AddAsync(order);

//            // Clear the OrderViewModel from session or other state management
//            HttpContext.Session.Remove("OrderViewModel");

//            // Redirect to the Order Confirmation page
//            return RedirectToAction("ViewOrders");
//        }

//        [HttpGet]
//        [Authorize]
//        public async Task<IActionResult> ViewOrders()
//        {
//            var userId = _userManager.GetUserId(User);

//            var userOrders = await _orders.GetAllByIdAsync(userId, "UserId", new QueryOptions<Order>
//            {
//                Includes = "OrderItems.Product"
//            });

//            return View(userOrders);
//        }



//    }
//}

//Code fixed 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using restaurant_prf.Data;
using restaurant_prf.Models;
using restaurant_prf.Models.OrderItemViewModels;
using TequilasRestaurant.Models;

namespace TequliasRestaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> _products;
        private Repository<Order> _orders;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _products = new Repository<Product>(context);
            _orders = new Repository<Order>(context);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Retrieve or create an OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddItem(int prodId, int prodQty)
        {
            var product = await _context.Products.FindAsync(prodId);
            if (product == null)
            {
                return NotFound();
            }

            // Retrieve or create an OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _products.GetAllAsync()
            };

            // Check if the product is already in the order
            var existingItem = model.OrderItems.FirstOrDefault(oi => oi.ProductId == prodId);

            // If the product is already in the order, update the quantity
            if (existingItem != null)
            {
                existingItem.Quantity += prodQty;
            }
            else
            {
                model.OrderItems.Add(new OrderItemViewModel
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = prodQty,
                    ProductName = product.Name
                });
            }

            // Update the total amount
            model.TotalAmount = model.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            // Save updated OrderViewModel to session
            HttpContext.Session.Set("OrderViewModel", model);

            // Redirect back to Create to show updated order items
            return RedirectToAction("Create", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            // Retrieve the OrderViewModel from session or other state management
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

            if (model == null || model.OrderItems == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

            if (model == null || model.OrderItems == null || model.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            // Create a new Order entity with initialized OrderItems list
            Order order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                UserId = _userManager.GetUserId(User),
                OrderItems = new List<OrderItem>()
            };

            // Add OrderItems to the Order entity AND reduce stock
            foreach (var item in model.OrderItems)
            {
                // Add item to order
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });

                // ✅ NEW: Reduce stock for this product
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;

                    // Prevent negative stock
                    if (product.Stock < 0)
                    {
                        product.Stock = 0;
                    }

                    _context.Products.Update(product);
                }
            }

            // Save the Order entity to the database
            await _orders.AddAsync(order);

            // ✅ NEW: Save stock changes to database
            await _context.SaveChangesAsync();

            // Clear the OrderViewModel from session or other state management
            HttpContext.Session.Remove("OrderViewModel");

            // Redirect to the Order Confirmation page
            return RedirectToAction("ViewOrders");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewOrders()
        {
            var userId = _userManager.GetUserId(User);

            var userOrders = await _orders.GetAllByIdAsync(userId, "UserId", new QueryOptions<Order>
            {
                Includes = "OrderItems.Product"
            });

            return View(userOrders);
        }
    }
}