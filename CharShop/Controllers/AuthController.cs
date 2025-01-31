using CharShop.DTO;
using CharShop.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}