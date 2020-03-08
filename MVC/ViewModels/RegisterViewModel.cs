using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong Password")]
        public string ConfirmPassword { get; set; }
    }
}