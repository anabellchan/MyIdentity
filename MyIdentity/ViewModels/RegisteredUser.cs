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
        public string Email { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$",
        ErrorMessage = "This is not a valid phone number.")]
        public string Phone { get; set; }

        [Display(Name = "City")]

        public string City { get; set; }

        [Display(Name = "State / Province")]
        public string Province { get; set; }
        
        
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [StringLength (30, MinimumLength=8, 
        ErrorMessage="Password has minimum length of 8, maximum of 30")]
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
