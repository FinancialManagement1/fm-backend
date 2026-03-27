using BusinessLogic.Interfaces;
using BusinessLogic.Mappers;
using BusinessLogic.Models;
using DAL.Entities;
using DAL.Enum;
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

        public async Task<TransactionResponse> CreateTransactionAsync(int userId, CreateTransactionRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            TransactionType transactionType = request.Type.Trim().ToLower() switch
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

            var newTransaction = await _transactionRepository.CreateAsync(transaction);

            return TransactionResponseMapper.ToResponse(newTransaction);
        }

        public async Task<List<TransactionResponse>> GetTransactionsAsync(int userId, GetTransactionsQuery query)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            TransactionType? transactionType = query.Type?.Trim().ToLower() switch
            {
                null => null,
                "" => null,
                "income" => TransactionType.Income,
                "expense" => TransactionType.Expense,
                _ => throw new Exception("Invalid transaction type.")
            };

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (!string.IsNullOrWhiteSpace(query.Period))
            {
                var parts = query.Period.Split('-');
                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);

                startDate = new DateTime(year, month, 1);
                endDate = startDate.Value.AddMonths(1);
            }

            var transactions = await _transactionRepository.GetFilteredAsync(
                userId,
                transactionType,
                query.Category,
                startDate,
                endDate);

            return transactions.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Type = t.Type == TransactionType.Income ? "income" : "expense",
                Amount = t.Amount,
                Currency = t.Currency,
                Category = t.Category,
                Description = t.Description,
                Date = t.Date
            }).ToList();
        }
        public async Task UpdateTransactionAsync(int transactionId, int userId, UpdateTransactionRequest request)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId);

            if (transaction == null || transaction.UserId != userId)
                throw new Exception("Transaction not found.");

            TransactionType type = request.Type.Trim().ToLower() switch
            {
                "income" => TransactionType.Income,
                "expense" => TransactionType.Expense,
                _ => throw new Exception("Invalid transaction type.")
            };

            transaction.Type = type;
            transaction.Amount = request.Amount;
            transaction.Currency = request.Currency ?? transaction.Currency;
            transaction.Category = request.Category;
            transaction.Description = request.Description;
            transaction.Date = request.Date;

            await _transactionRepository.UpdateAsync(transaction);
        }
    }
}