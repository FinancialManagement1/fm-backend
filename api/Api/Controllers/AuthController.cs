using Api.DTOs;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
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
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "The server could not understand the request due to invalid syntax."
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

                return StatusCode(StatusCodes.Status201Created, response);

            }
            catch (EmailAlreadyExistsException ex)
            {
                return StatusCode(409, new ErrorResponseDto
                {
                    Message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "The server encountered an unexpected condition that prevented it from fulfilling the request."
                });
            }
        }

        // POST: /auth/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Message = "The server could not understand the request due to invalid syntax."
                });
            }
            try
            {
                var response = await _authService.LoginAsync(loginRequestDto.Email, loginRequestDto.Password);
                return Ok(response);
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new ErrorResponseDto
                {
                    Message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "The server encountered an unexpected condition that prevented it from fulfilling the request."
                });
            }
        }
    }
}