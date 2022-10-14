using Microsoft.EntityFrameworkCore;
using PizzaWebApp.Models;

namespace PizzaWebApp.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Contact> Contact { get; set; }

        public DbSet<Bookings> Bookings { get; set; }


    }
}