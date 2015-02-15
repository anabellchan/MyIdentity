using System.ComponentModel.DataAnnotations;

namespace MyIdentity.ViewModels
{
    public class RegisteredUser
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
        ErrorMessage = "This is not a valid email address.")]
        public string myEmail { get; set; }

        [Display(Name = "Street Address")]
        [DataType(DataType.Text)]
        public string myAddress { get; set; }

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "City")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Display(Name = "State / Province")]
        [DataType(DataType.Text)]
        public string Province { get; set; }
        
        
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
