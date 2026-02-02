using MaisonTelecom.Models;
using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.ViewModels
{
    public class AdminAddProductViewModel
    {
        [Required]
        public string Name { get; set; }

        // These will store the selected value
        public string Brand { get; set; }
        public string ProductCategory { get; set; }
        public string RAM { get; set; }
        public string ROM { get; set; }
        public string Processor { get; set; }
        public string Display { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? PromoPrice { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public string BasicSpecs { get; set; }
        public string DisplayProperties { get; set; }
        public string SpecialFeatures { get; set; }

        public string ImageURL1 { get; set; }
        public string ImageURL2 { get; set; }
        public string ImageURL3 { get; set; }
        public string ImageURL4 { get; set; }
        public string ImageURL5 { get; set; }

        public bool IsTrending { get; set; }
        public bool IsLatest { get; set; }
        public ProductType Type { get; set; } = ProductType.Physical;

        // --- EMPTY LISTS (Populated by DB) ---
        public List<string> BrandOptions { get; set; } = new();
        public List<string> CategoryOptions { get; set; } = new();
        public List<string> RAMOptions { get; set; } = new();
        public List<string> ROMOptions { get; set; } = new();
        public List<string> ProcessorOptions { get; set; } = new();
        public List<string> DisplayOptions { get; set; } = new();

        public Product ToProduct()
        {
            return new Product
            {
                Name = this.Name,
                Brand = this.Brand,
                ProductCategory = this.ProductCategory,
                Price = this.Price,
                PromoPrice = this.PromoPrice,
                RAM = this.RAM,
                ROM = this.ROM,
                Processor = this.Processor,
                Display = this.Display,
                StockQuantity = this.StockQuantity,
                BasicSpecs = this.BasicSpecs,
                DisplayProperties = this.DisplayProperties,
                SpecialFeatures = this.SpecialFeatures,
                ImageURL1 = this.ImageURL1,
                ImageURL2 = this.ImageURL2,
                ImageURL3 = this.ImageURL3,
                ImageURL4 = this.ImageURL4,
                ImageURL5 = this.ImageURL5,
                IsTrending = this.IsTrending,
                IsLatest = this.IsLatest
            };
        }
    }
}