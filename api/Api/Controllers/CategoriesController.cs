using Api.DTOs;
using BusinessLogic.Models;
using DAL.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Api.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new ErrorResponse { Message = "Unauthorized. Missing or invalid JWT token." });
            }

            try 
            {
                // In a real application, you would fetch categories from a database based on the userId.
                // For this example, we will return a static list of categories.
                var items = Enum.GetValues(typeof(TransactionType))
                    .Cast<TransactionType>()
                    .Select(t => new CategoryItem
                    {
                        Type = t.ToString().ToLowerInvariant(),
                        Name = t.ToString()
                    })
                    .ToList();

                return Ok(items);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, new ErrorResponse { Message = "An error occurred while fetching categories." });
            }
        }
    }
}
