using System.ComponentModel.DataAnnotations;
using MaisonTelecom.Models;

namespace MaisonTelecom.ViewModels
{
    public class AdminAddServiceViewModel
    {
        [Required]
        public string Name { get; set; } // e.g. "iPhone Screen Repair"

        [Required]
        public string Category { get; set; } // "Hardware", "Software", "Unlock"

        [Required]
        [Range(100, int.MaxValue, ErrorMessage = "Price must be at least 100 FCFA")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } // Maps to 'BasicSpecs' in DB

        public string Features { get; set; } // Maps to 'SpecialFeatures' (comma separated)

        public bool IsPopular { get; set; } // Maps to 'IsTrending'
        public decimal? MaxPrice { get; set; }

        // Helper to convert this view model to the Database Model
        public Product ToProduct()
        {
            return new Product
            {
                Name = this.Name,
                ProductCategory = this.Category, // We use this for filtering
                Price = this.Price,
                BasicSpecs = this.Description,
                SpecialFeatures = this.Features,
                IsTrending = this.IsPopular,
                MaxPrice = this.MaxPrice,
                // DEFAULTS FOR SERVICE
                Type = ProductType.Service, // Critical: Marks it as a Service
                StockQuantity = 999,        // Infinite stock
                Brand = "Service",          // Placeholder
                ImageURL1 = "/Image/maisonlogo.jpg" // Default placeholder image
            };
        }
    }
}