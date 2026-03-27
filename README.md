# Employee Management System API

## 🚀 Features
- CRUD Operations for Employees
- JWT Authentication
- Role-based Authorization (Admin / Employee)
- Pagination & Filtering
- Employee Statistics
- Export to Excel
- Clean Architecture
- Repository Pattern & Unit of Work

## 🛠 Technologies
- .NET 8
- Entity Framework Core (Database First)
- SQL Server
- AutoMapper
- FluentValidation
- JWT Authentication

## 🔐 Authentication
Use /api/Auth/login to get token then use:
Bearer {token}

## 📊 Endpoints
- GET /api/employees
- GET /api/employees/stats
- GET /api/employees/export

## ⚙️ Setup
1. Update connection string in appsettings.json
2. Run project
3. Swagger: /swagger
