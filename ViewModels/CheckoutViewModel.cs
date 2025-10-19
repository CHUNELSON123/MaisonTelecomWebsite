using System.ComponentModel.DataAnnotations;

namespace MaisonTelecom.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        // Address fields are optional
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Address3 { get; set; } = string.Empty;

        [Required]
        public string TownCity { get; set; } = string.Empty;

        // Region is optional
        public string Region { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a payment method")]
        public string PaymentMethod { get; set; } = string.Empty;
    }
}