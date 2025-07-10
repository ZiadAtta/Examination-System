using System.ComponentModel.DataAnnotations;

namespace Examination_System.DTOS
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(50, ErrorMessage = "UserName cannot be longer than 50 characters.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "PasswordConfirm is required.")]
        [StringLength(20, ErrorMessage = "PasswordConfirm must be at least 6 characters long.", MinimumLength = 6)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string? PasswordConfirm { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        public string? Role { get; set; }
    }
}
