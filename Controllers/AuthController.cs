using Microsoft.AspNetCore.Mvc;
using YourProject.Models;
using YourProject.Repositories;
using YourProject.Services;
using System.Threading.Tasks;

namespace YourProject.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _authRepository;
        private readonly JwtService _jwtService;

        public AuthController(AuthRepository authRepository, JwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Password is required.");
            }

            var createdUser = await _authRepository.Register(user, user.Password);
            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var existingUser = await _authRepository.Login(user.UserName, user.Password);
            if (existingUser == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwtService.GenerateToken(existingUser);
            return Ok(new { Token = token });
        }
    }
}
