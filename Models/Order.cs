using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisonTelecom.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Made nullable (int?) so you can have Guest Checkout (no account required)
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // --- THESE ARE THE NEW FIELDS YOU WERE MISSING ---
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ShippingAddress { get; set; }
        // -------------------------------------------------

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; } = "";
        public string Status { get; set; } = "Pending";
    }
}