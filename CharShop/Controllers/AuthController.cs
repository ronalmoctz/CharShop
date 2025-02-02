using CharShop.DTO;
using CharShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CharShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);

            if (result != null)
            {
                return BadRequest(new { message = result });
            }

            return Ok(new { message = "User registered successfully." });

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                Log.Warning("Login failed: invalid credentials");
                return Unauthorized();
            }

            return Ok(new { token, message = "Login successful, welcome" });
        }
    }
}