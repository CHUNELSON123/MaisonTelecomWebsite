using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MaisonTelecom.Models;
using Microsoft.AspNetCore.Identity;

namespace MaisonTelecom.Data
{
    // Change inheritance from DbContext to IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<RepairTicket> RepairTickets { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}