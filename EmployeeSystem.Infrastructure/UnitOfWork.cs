using EmployeeSystem.Application.Interfaces;
using EmployeeSystem.Infrastructure.Data;
using EmployeeSystem.Infrastructure.Models;
using EmployeeSystem.Infrastructure.Repositories;
namespace EmployeeSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeDbContext _context;

        public IGenericRepository<Employee> Employees { get; }
        public IGenericRepository<AppUser> Users { get; }
        public UnitOfWork(EmployeeDbContext context)
        {
            _context = context;
            Employees = new GenericRepository<Employee>(_context);
           Users = new GenericRepository<AppUser>(_context);
        }

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();
    }
}
