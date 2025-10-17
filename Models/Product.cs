// MaisonTelecom/Models/Product.cs
using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Brand { get; set; }

        public string? ProductCategory { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string? RAM { get; set; }
        public string? ROM { get; set; }
        public string? Processor { get; set; }
        public string? Display { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive number")]
        public int StockQuantity { get; set; }

        public string? BasicSpecs { get; set; }
        public string? DisplayProperties { get; set; }
        public string? SpecialFeatures { get; set; }

        // These are now nullable
        public string? ImageURL1 { get; set; }
        public string? ImageURL2 { get; set; }
        public string? ImageURL3 { get; set; }
        public string? ImageURL4 { get; set; }
        public string? ImageURL5 { get; set; }

        public bool IsTrending { get; set; }
        public bool IsLatest { get; set; }
    }
}