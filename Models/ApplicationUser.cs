using Microsoft.AspNetCore.Identity;

namespace restaurant_prf.Models
{
    public class ApplicationUser : IdentityUser 

    {
        public ICollection<Order>? Orders { get; set; }
    }
}
