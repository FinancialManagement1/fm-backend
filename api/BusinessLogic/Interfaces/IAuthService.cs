using BusinessLogic.Models;
using DAL.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(string name, string email, string password, string country, string preferredCurrency);
        Task<AuthResponseDto> LoginAsync(string email, string password);
    }
}