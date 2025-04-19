using MoodLoggerApi.Models.DTOs;
using MoodLoggerApi.Models;

namespace MoodLoggerApi.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
