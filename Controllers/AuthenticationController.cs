using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoodLoggerApi.Models.DTOs;
using MoodLoggerApi.Services;

namespace MoodLoggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Model validasyonu (DTO üzerindeki [Required] vb. attribute'lar)
            // ASP.NET Core tarafından otomatik yapılır, ModelState.IsValid ile kontrol edilebilir.
            // Ancak [ApiController] attribute'u bunu otomatik yapar ve geçersizse 400 döner.

            var createdUser = await _authService.RegisterAsync(registerDto);

            if (createdUser == null)
            {
                // AuthService null döndüyse, muhtemelen e-posta zaten kayıtlıdır.
                //ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor."); // Daha açıklayıcı hata
                //return ValidationProblem(ModelState);
                return BadRequest(new { message = "Bu e-posta adresi zaten kullanılıyor." });
            }

            // Başarılı kayıt sonrası kullanıcı bilgilerini veya sadece bir başarı mesajı dönebiliriz.
            // 201 Created ile birlikte oluşturulan kaynağın (kullanıcının) nerede bulunabileceğini
            // Location header'ı ile belirtmek iyi bir pratiktir, ama şimdilik basit tutalım.
            // Örneğin: return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser); (Eğer bir GetUserById action'ınız varsa)
            return StatusCode(StatusCodes.Status201Created, new { message = "Kullanıcı başarıyla oluşturuldu." });
            // Veya sadece Ok("Kullanıcı başarıyla oluşturuldu.") da dönebilirsiniz.
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Geçersiz model için
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResponse = await _authService.LoginAsync(loginDto);

            if (loginResponse == null)
            {
                // AuthService null döndüyse, e-posta/şifre yanlıştır.
                return Unauthorized(new { message = "Geçersiz e-posta veya şifre." });
            }

            // Başarılı girişte token'ı içeren DTO'yu dön
            return Ok(loginResponse);
        }

        // Örnek: Yetkilendirme gerektiren bir endpoint testi
        [HttpGet("testauth")]
        [Authorize] // Bu action'a erişim için geçerli bir JWT token gerekir
        public IActionResult TestAuthorization()
        {
            // Token geçerliyse, buraya erişilebilir.
            // Controller'da User property'sinden claim'lere erişilebilir.
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            return Ok($"Yetkilendirme başarılı! UserId: {userId}, Email: {userEmail}");
        }
    }
}

