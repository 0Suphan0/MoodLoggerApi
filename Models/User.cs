using System.ComponentModel.DataAnnotations;

namespace MoodLoggerApi.Models
{
   
        public class User
        {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } // Şifre hash'lenmiş olacak

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Kayıt tarihi
    }

    
}
