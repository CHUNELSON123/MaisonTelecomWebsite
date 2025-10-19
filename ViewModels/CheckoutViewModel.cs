using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string Address3 { get; set; }

        [Required]
        public string TownCity { get; set; }

        [Required]
        public string Region { get; set; }
    }
}