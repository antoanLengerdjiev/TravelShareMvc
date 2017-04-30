namespace TravelShare.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "FirstNameValidation", MinimumLength = 2)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "LastNameValidation", MinimumLength = 2)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = Common.GlobalConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
