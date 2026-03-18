using BCrypt.Net;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DAL.Entities;
using DAL.Repositories;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> RegisterAsync(string name, string email, string password, string country, string preferredCurrency)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                throw new EmailAlreadyExistsException();
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            await _userRepository.CreateAsync(new User
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                Country = country,
                PreferredCurrency = preferredCurrency,
                CreatedAt = DateTime.UtcNow
            });

            var response = new AuthResponseDto
            {
                Token = "fake-jwt-token",
                ExpiresIn = 3600,
                Message = "User successfully registered"
            };

            return response;
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email) ?? throw new InvalidCredentialsException();
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialsException();
            }

            var response = new AuthResponseDto
            {
                Token = "fake-jwt-token",
                ExpiresIn = 3600,
                Message = "Authentication successful"
            };

            return response;
        }
    }
}