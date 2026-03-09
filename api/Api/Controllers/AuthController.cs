using Microsoft.AspNetCore.Mvc;
using Api.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        // POST: /auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (request == null) 
            {
                return StatusCode(400,new {message = "The server could not understand the request due to invalid syntax." });
            }

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return StatusCode(400,new { message = "Email and password are required" });
            }

            // Fake duplicate email check
            if (request.Email == "existing@test.com")
            {
                return StatusCode(409,new { message = "Email already exists" });
            }

            var response = new AuthResponse
            {
                Token = "fake-jwt-token",
                ExpiresIn = 3600,
                Message = "User successfully registered"
            };

            return StatusCode(201, response);
        }

        // POST: /auth/login
           [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return StatusCode(400, new
                {
                    message = "The server could not understand the request due to invalid syntax."
                });
            }

            if (string.IsNullOrWhiteSpace(request.email) || string.IsNullOrWhiteSpace(request.password))
            {
                return StatusCode(400, new
                {
                    message = "Email and password are required"
                });
            }

            // Fake login check
            if (request.email != "test@test.com" || request.password != "Password123")
            {
                return StatusCode(401, new
                {
                    message = "Invalid email or password"
                });
            }

            var response = new AuthResponse
            {
                Token = "fake-jwt-token",
                ExpiresIn = 3600,
                Message = "Login successful"
            };

            return StatusCode(200, response);
        }
    }
}
