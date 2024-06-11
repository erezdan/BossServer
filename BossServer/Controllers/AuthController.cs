using BossServer.Models;
using BossServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BossServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUser newUser)
        {
            var success = await _userService.RegisterAsync(newUser);
            if (!success)
            {
                return BadRequest("User already exists.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate token or perform other actions to authenticate the user
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            // Generate JWT token
            // ...
            return "token";
        }
    }
}
