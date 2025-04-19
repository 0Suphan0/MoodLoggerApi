using System.ComponentModel.DataAnnotations;

namespace MoodLoggerApi.Models.DTOs
{
    public class LoginDto
    {

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Password { get; set; } = null!;
    }
}
