using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } // e.g., "Brand", "Category", "RAM"

        [Required]
        public string Value { get; set; } // e.g., "Apple", "Phones", "8GB"
    }
}