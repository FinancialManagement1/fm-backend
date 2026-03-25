using Api.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpPost("transactions")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "The server could not understand the request due to invalid syntax.",
                    errors = ModelState
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "Missing or invalid JWT token." });
            }

            try
            {
                await _transactionService.CreateTransactionAsync(userId, request);

                return StatusCode(201, new
                {
                    message = "Transaction created successfully"
                });
            }
            catch (Exception)
            {
                return BadRequest( new ErrorResponse { Message = "The server encountered an unexpected condition that prevented it from fullfilling the request" });
            }
            
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            // Implementation for retrieving transactions
            return Ok();
        }
    }
}
