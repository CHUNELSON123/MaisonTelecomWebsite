using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public enum ProductType
    {
        Physical = 0,
        Service = 1
    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Default to empty

        public string? Brand { get; set; }
        public string? ProductCategory { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? PromoPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? RAM { get; set; }
        public string? ROM { get; set; }
        public string? Processor { get; set; }
        public string? Display { get; set; }

        public ProductType Type { get; set; } = ProductType.Physical;

        [Required]
        public int StockQuantity { get; set; }

        public string? BasicSpecs { get; set; }
        public string? DisplayProperties { get; set; }
        public string? SpecialFeatures { get; set; }

        public string? ImageURL1 { get; set; }
        public string? ImageURL2 { get; set; }
        public string? ImageURL3 { get; set; }
        public string? ImageURL4 { get; set; }
        public string? ImageURL5 { get; set; }

        public string? IconClass { get; set; }

        public bool IsTrending { get; set; }
        public bool IsLatest { get; set; }
    }
}