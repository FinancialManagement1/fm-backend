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
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategories()
        {
            var items = new List<CategoryItem>
            {
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Food" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Transport" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Shopping" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Bills" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Entertainment" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Health" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Education" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Insurance" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Mortgage" },
                new CategoryItem { Type = TransactionType.Expense.ToString().ToLowerInvariant(), Name = "Other" },

                new CategoryItem { Type = TransactionType.Income.ToString().ToLowerInvariant(), Name = "Salary" },
                new CategoryItem { Type = TransactionType.Income.ToString().ToLowerInvariant(), Name = "Freelance" },
                new CategoryItem { Type = TransactionType.Income.ToString().ToLowerInvariant(), Name = "Business" },
                new CategoryItem { Type = TransactionType.Income.ToString().ToLowerInvariant(), Name = "Investment" },
                new CategoryItem { Type = TransactionType.Income.ToString().ToLowerInvariant(), Name = "Gift" },
                new CategoryItem { Type = TransactionType.Income.ToString().ToLowerInvariant(), Name = "Other" }
            };

            return Ok(new { items });
        }
    }
}