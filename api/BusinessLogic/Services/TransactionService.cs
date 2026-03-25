using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BusinessLogic.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(
            IUserRepository userRepository,
            ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task CreateTransactionAsync(int userId, CreateTransactionRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            TransactionType transactionType = request.Type.ToLower() switch
            {
                "income" => TransactionType.Income,
                "expense" => TransactionType.Expense,
                _ => throw new Exception("Invalid transaction type.")
            };

            var transaction = new Transaction
            {
                UserId = userId,
                Amount = request.Amount,
                Currency = string.IsNullOrWhiteSpace(request.Currency)
                    ? user.PreferredCurrency
                    : request.Currency,
                Category = request.Category,
                Description = request.Description,
                Date = request.Date,
                Type = transactionType
            };

            await _transactionRepository.CreateAsync(transaction);
        }
    }
}