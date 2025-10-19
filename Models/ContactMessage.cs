using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Message is too long.")]
        public string Message { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
    }
}