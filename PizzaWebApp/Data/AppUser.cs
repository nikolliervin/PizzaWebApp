using Microsoft.AspNetCore.Identity;

namespace PizzaWebApp.Data
{
    public class AppUser : IdentityUser<int>
    {
        public int FirstName { get; set; }

        public int LastName { get; set; }
    }
}
