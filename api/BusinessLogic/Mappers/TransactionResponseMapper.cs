using BusinessLogic.Models;
using DAL.Entities;
using DAL.Enum;
using System.Globalization;

namespace BusinessLogic.Mappers
{
    public class TransactionResponseMapper
    {
        public static TransactionResponse ToResponse(Transaction transaction) 
        {
            return new TransactionResponse
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Type = transaction.Type.ToString().ToLowerInvariant(),
                Currency = transaction.Currency,
                Category = transaction.Category,
                Description = transaction.Description,
                Date = transaction.Date.ToString("yyyy-MM-dd"),
                CreatedAt = transaction.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                UpdatedAt = transaction.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            };
        }
    }
}