using MaisonTelecom.Data;
using MaisonTelecom.Models;
using Microsoft.EntityFrameworkCore;

namespace MaisonTelecom.Services
{
    public class ProductService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public ProductService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task AddProductAsync(Product product)
        {
            using var context = await _factory.CreateDbContextAsync();
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetTrendingProductsAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Products
                .Where(p => p.IsTrending && p.Type == ProductType.Physical)
                .Take(9)
                .ToListAsync();
        }

        public async Task<List<Product>> GetLatestProductsAsync(string category)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Products
                .Where(p => p.IsLatest && p.ProductCategory == category && p.Type == ProductType.Physical)
                .Take(9)
                .ToListAsync();
        }

        public async Task<List<Product>> GetBrandProductsAsync(string brandName)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Products
                .Where(p => p.Brand == brandName && p.Type == ProductType.Physical)
                .Take(9)
                .ToListAsync();
        }

        public async Task<List<Product>> GetServicesAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Products
                .Where(p => p.Type == ProductType.Service)
                .ToListAsync();
        }

        // --- ATTRIBUTE METHODS (For Dropdowns) ---
        public async Task<List<string>> GetAttributeValuesAsync(string type)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.ProductAttributes
                .Where(a => a.Type == type)
                .OrderBy(a => a.Value)
                .Select(a => a.Value)
                .ToListAsync();
        }

        public async Task AddAttributeAsync(string type, string value)
        {
            using var context = await _factory.CreateDbContextAsync();
            if (!await context.ProductAttributes.AnyAsync(a => a.Type == type && a.Value == value))
            {
                context.ProductAttributes.Add(new ProductAttribute { Type = type, Value = value });
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAttributeAsync(string type, string value)
        {
            using var context = await _factory.CreateDbContextAsync();
            var attr = await context.ProductAttributes.FirstOrDefaultAsync(a => a.Type == type && a.Value == value);
            if (attr != null)
            {
                context.ProductAttributes.Remove(attr);
                await context.SaveChangesAsync();
            }
        }

        // --- REVIEW METHODS (Fixes missing definition error) ---
        public async Task<List<Review>> GetRecentReviewsAsync()
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Reviews
                .OrderByDescending(r => r.DatePosted)
                .Take(6)
                .ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            using var context = await _factory.CreateDbContextAsync();
            context.Reviews.Add(review);
            await context.SaveChangesAsync();
        }
    }
}