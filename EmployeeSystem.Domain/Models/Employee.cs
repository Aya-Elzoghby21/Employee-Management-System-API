using System;
using System.Collections.Generic;

namespace EmployeeSystem.Infrastructure.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }

    public string? ImageUrl { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
