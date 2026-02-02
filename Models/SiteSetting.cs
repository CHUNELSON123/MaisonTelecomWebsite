using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.Models
{
    public class SiteSetting
    {
        [Key]
        public string Key { get; set; } // e.g., "WhatsAppNumber", "FacebookLink"
        public string Value { get; set; } // The actual number or link
    }
}