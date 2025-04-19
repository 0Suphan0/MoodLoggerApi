namespace MoodLoggerApi.Models.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!; // İsteğe bağlı: Kullanıcı bilgilerini de dönebilirsiniz
        public string FullName { get; set; } = null!; // İsteğe bağlı
    }

}
