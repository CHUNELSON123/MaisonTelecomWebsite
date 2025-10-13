using MaisonTelecom.Models;
using Microsoft.EntityFrameworkCore;

namespace MaisonTelecom.Data
{
    public class MaisonTelecomContext : DbContext
    {
        public MaisonTelecomContext(DbContextOptions<MaisonTelecomContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}