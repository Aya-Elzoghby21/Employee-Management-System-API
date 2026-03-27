namespace EmployeeSystem.Application.DTOs
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }
    }
}
