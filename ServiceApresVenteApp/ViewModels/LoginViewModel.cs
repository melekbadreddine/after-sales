using System.ComponentModel.DataAnnotations;

namespace ServiceApresVenteApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        // [RegularExpression(@"^[a-zA-Z0-9_]+@[a-zA-Z0-9_]+\.[a-zA-Z0-9_]+$")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
