using System.ComponentModel.DataAnnotations.Schema;

namespace MaisonTelecom.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        // --- THESE ARE THE NEW FIELDS YOU WERE MISSING ---
        public string? ProductName { get; set; } // Snapshot of the name

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }       // Renamed from UnitPrice to Price
        // -------------------------------------------------
    }
}