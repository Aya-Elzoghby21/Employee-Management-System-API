using System;
using System.Collections.Generic;

namespace EmployeeSystem.Infrastructure.Models;

public partial class AppUser
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }
}
