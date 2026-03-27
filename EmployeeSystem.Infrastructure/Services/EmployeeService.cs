using AutoMapper;
using ClosedXML.Excel;
using EmployeeSystem.Application.Common;
using EmployeeSystem.Application.DTOs;
using EmployeeSystem.Application.Interfaces;
using EmployeeSystem.Infrastructure.Helpers;
using EmployeeSystem.Infrastructure.Models;
namespace EmployeeSystem.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<EmployeeDto>>> GetAllAsync(EmployeeQueryParams query)
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            if (query.FromDate.HasValue)
                employees = employees.Where(x => x.HireDate >= query.FromDate);

            if (query.ToDate.HasValue)
                employees = employees.Where(x => x.HireDate <= query.ToDate);

            var result = employees
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var mapped = _mapper.Map<List<EmployeeDto>>(result);

            return ApiResponse<List<EmployeeDto>>.Success(mapped);
        }
      
        public async Task<ApiResponse<EmployeeDto>> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return ApiResponse<EmployeeDto>.Fail("Employee not found");

            var mapped = _mapper.Map<EmployeeDto>(employee);

            return ApiResponse<EmployeeDto>.Success(mapped);
        }
        public async Task<ApiResponse<string>> AddAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            var imageName = await FileHelper.SaveImageAsync(dto.Image, imageFolder);
            employee.ImageUrl = imageName != null ? "/images/" + imageName : null;
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<string>.Success("Employee added successfully");
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return ApiResponse<string>.Fail("Employee not found");

            _mapper.Map(dto, employee);

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<string>.Success("Employee updated successfully");
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                return ApiResponse<string>.Fail("Employee not found");

            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<string>.Success("Employee deleted successfully");
        }

        public async Task<EmployeeStatsDto> GetStats()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            var total = employees.Count();

            var thisMonth = employees.Count(e =>
                e.HireDate.Month == DateTime.Now.Month &&
                e.HireDate.Year == DateTime.Now.Year);

            return new EmployeeStatsDto
            {
                TotalEmployees = total,
                ThisMonthEmployees = thisMonth
            };
        }

        public async Task<byte[]> ExportToExcel()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Employees");

            sheet.Cell(1, 1).Value = "Name";
            sheet.Cell(1, 2).Value = "Hire Date";

            int row = 2;

            foreach (var emp in employees)
            {
                sheet.Cell(row, 1).Value = emp.Name;
                sheet.Cell(row, 2).Value = emp.HireDate;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}
