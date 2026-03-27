namespace EmployeeSystem.Application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        public string ImageUrl { get; set; }

        public int DepartmentId { get; set; }
    }
}
