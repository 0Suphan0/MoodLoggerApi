using MoodLoggerApi.Models;
using MoodLoggerApi.Models.DTOs;
using MoodLoggerApi.Data;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt; // Alias (takma ad) kullanmak iyi bir pratiktir




namespace MoodLoggerApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context; // DbContext'inizi enjekte edin
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<User?> RegisterAsync(RegisterDto registerDto)
        {
            // E-posta adresinin zaten kayıtlı olup olmadığını kontrol et
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                // E-posta zaten kullanımdaysa null dön (veya bir exception fırlat)
                // Controller katmanında bu durum ele alınacak (örn: BadRequest)
                return null;
            }

            // Şifreyi hash'le
            string passwordHash = BCryptNet.HashPassword(registerDto.Password);

            var newUser = new User
            {
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow // UTC kullanmak genellikle daha iyidir
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // Kullanıcıyı e-posta adresine göre bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            // Kullanıcı bulunamazsa veya şifre yanlışsa null dön
            if (user == null || !BCryptNet.Verify(loginDto.Password, user.PasswordHash))
            {
                return null; // Yetkisiz giriş denemesi
            }

            // Şifre doğruysa JWT oluştur
            var token = _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = $"{user.Name} {user.LastName}"
            };
        }
    }
}
