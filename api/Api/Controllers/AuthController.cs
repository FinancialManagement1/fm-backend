using Api.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: /auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "The server could not understand the request due to invalid syntax."
                });
            }

            try
            {
                var response = await _authService.RegisterAsync(
                    request.Name,
                    request.Email,
                    request.Password,
                    request.Country,
                    request.PreferredCurrency
                );

                return StatusCode(201, response);

            }
            catch (EmailAlreadyExistsException ex)
            {
                return StatusCode(409, new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "The server encountered an unexpected condition that prevented it from fulfilling the request."
                });
            }
        }

        // POST: /auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest( new
                {
                    message = "The server could not understand the request due to invalid syntax."
                });
            }
            try
            {
                var response = await _authService.LoginAsync(loginRequestDto.Email, loginRequestDto.Password);
                return Ok(response);
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized( new
                {
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "The server encountered an unexpected condition that prevented it from fulfilling the request."
                });
            }
        }
    }
}