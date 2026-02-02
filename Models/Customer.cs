using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        // Email is optional for WhatsApp orders, so it can be nullable
        public string? Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? TownCity { get; set; }
        public string? Region { get; set; }

        // Navigation property: A customer can have multiple orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}