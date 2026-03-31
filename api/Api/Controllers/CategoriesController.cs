using BusinessLogic.Models;
using DAL.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("categories")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategories()
        {
            var items = Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(t => new CategoryItem
                {
                    Type = t.ToString().ToLowerInvariant(),
                    Name = t.ToString()
                })
                .ToList();

            return Ok(new { items });
        }
    }
}
