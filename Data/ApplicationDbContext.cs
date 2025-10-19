// MaisonTelecom/Data/ApplicationDbContext.cs
using MaisonTelecom.Models;
using Microsoft.EntityFrameworkCore;

namespace MaisonTelecom.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
    }
}