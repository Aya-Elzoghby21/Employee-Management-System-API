using EmployeeSystem.Application.DTOs;
using EmployeeSystem.Application.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
namespace EmployeeSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = (await _unitOfWork.Users
                .FindAsync(u => u.Username == dto.Username))
                .FirstOrDefault();

            if (user == null)
                throw new Exception("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.Now.AddHours(1)
            };
        }
    }
    }