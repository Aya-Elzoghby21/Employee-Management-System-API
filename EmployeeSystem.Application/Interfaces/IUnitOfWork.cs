using EmployeeSystem.Infrastructure.Models;
namespace EmployeeSystem.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> Employees { get; }
       IGenericRepository<AppUser> Users { get; }
        Task<int> CompleteAsync();
    }
}
