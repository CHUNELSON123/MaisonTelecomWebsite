// MaisonTelecom/ViewModels/AdminAddProductViewModel.cs
using MaisonTelecom.Models;
using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.ViewModels
{
    public class AdminAddProductViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Brand { get; set; }
        public string ProductCategory { get; set; } // New Property

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string RAM { get; set; }
        public string ROM { get; set; }
        public string Processor { get; set; }
        public string Display { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive number")]
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

        // Options for Dropdowns
        public List<string> BrandOptions { get; set; } = new List<string> { "Apple", "Samsung", "Google", "OnePlus" };
        public List<string> CategoryOptions { get; set; } = new List<string> { "Phones", "Laptops", "Accessories" };
        public List<string> RAMOptions { get; set; } = new List<string> { "4GB", "6GB", "8GB", "12GB", "16GB" };
        public List<string> ROMOptions { get; set; } = new List<string> { "64GB", "128GB", "256GB", "512GB", "1TB" };
        public List<string> ProcessorOptions { get; set; } = new List<string> { "Snapdragon", "A15 Bionic", "Tensor", "Dimensity" };
        public List<string> DisplayOptions { get; set; } = new List<string> { "AMOLED", "OLED", "LCD", "Retina" };

        public Product ToProduct()
        {
            return new Product
            {
                Name = this.Name,
                Brand = this.Brand,
                ProductCategory = this.ProductCategory,
                Price = this.Price,
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