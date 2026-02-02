using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class RepairTicket
    {
        public int Id { get; set; }

        [Required]
        // This is the "Magic Number" the customer types on the website
        // Format example: "REP-9052"
        public string TicketNumber { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string DeviceModel { get; set; } // e.g., "Samsung A12"

        [Required]
        public string IssueDescription { get; set; } // e.g., "Cracked Screen, Black spots"

        // Status: "Checked In", "Diagnosing", "Waiting for Parts", "Ready for Pickup", "Collected"
        public string Status { get; set; } = "Checked In";

        // The "Trust Builder" - Notes visible to the customer
        // e.g., "Opened device. Found water damage near charging port. Cleaning now."
        public string? TechnicianPublicNotes { get; set; }

        // Private notes for you (e.g., "Battery looks fake, don't warranty it")
        public string? InternalNotes { get; set; }

        public decimal EstimatedCost { get; set; }
        public decimal FinalCost { get; set; }

        public DateTime DateReceived { get; set; } = DateTime.Now;
        public DateTime? DateCompleted { get; set; }
    }
}