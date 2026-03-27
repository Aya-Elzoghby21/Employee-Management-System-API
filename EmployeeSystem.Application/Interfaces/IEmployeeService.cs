using EmployeeSystem.Application.Common;
using EmployeeSystem.Application.DTOs;
namespace EmployeeSystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse<List<EmployeeDto>>> GetAllAsync(EmployeeQueryParams query);
        Task<ApiResponse<EmployeeDto>> GetByIdAsync(int id);
        Task<ApiResponse<string>> AddAsync(CreateEmployeeDto dto);
        Task<ApiResponse<string>> UpdateAsync(int id, UpdateEmployeeDto dto);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<EmployeeStatsDto> GetStats();
        Task<byte[]> ExportToExcel();
    }
}
