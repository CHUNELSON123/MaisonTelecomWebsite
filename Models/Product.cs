namespace MaisonTelecom.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public decimal Price { get; set; }
        public string? Ram { get; set; }
        public string? Rom { get; set; }
        public string? Processor { get; set; }
        public string? Display { get; set; }
        public int StockQuantity { get; set; }
        public string? BasicSpecs { get; set; }
        public string? DisplayProperties { get; set; }
        public string? SpecialFeatures { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public string? ImageUrl4 { get; set; }
        public string? ImageUrl5 { get; set; }
        public bool IsTrending { get; set; }
        public bool IsLatest { get; set; }
    }
}