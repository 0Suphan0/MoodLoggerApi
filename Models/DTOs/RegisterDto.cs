using System.ComponentModel.DataAnnotations;

namespace MoodLoggerApi.Models.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "İsim en fazla 100 karakter olabilir.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Soyisim alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Soyisim en fazla 100 karakter olabilir.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; } = null!;


    }
}
