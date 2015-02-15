using System.ComponentModel.DataAnnotations;

namespace MyIdentity.ViewModels
{
    public class Login
    {
        [Required]
        [Display(Name = "User Name")]
        public string myUser { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
