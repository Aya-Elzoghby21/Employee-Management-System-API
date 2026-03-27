using EmployeeSystem.Infrastructure.Models;

namespace EmployeeSystem.Application.Interfaces
{
    public interface IJwtService
    {
       string GenerateToken(AppUser user);
    }
}
