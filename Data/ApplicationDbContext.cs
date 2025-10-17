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
    }
}