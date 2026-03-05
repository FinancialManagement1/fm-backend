using Microsoft.AspNetCore.Mvc;
using Api.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
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

            var response = new RegisterResponse
            {
                Token = "fake-jwt-token",
                ExpiresIn = 3600,
                Message = "User successfully registered"
            };

            return StatusCode(201, response);
        }
    }
}
