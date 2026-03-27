using EmployeeSystem.Application.DTOs;
namespace EmployeeSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}
