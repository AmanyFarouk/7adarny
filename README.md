# 7adarny - Educational Center Management API

7adarny is a backend API designed to help teachers manage educational centers, study groups, and student attendance.

The system focuses on **group management, student enrollment, attendance tracking, and reporting**, while following modern backend architecture practices.

The project is built using **Clean Architecture + CQRS** to ensure scalability, maintainability, and clear separation of responsibilities.

---

# 🚀 Technologies Used

- .NET 8
- ASP.NET Core Web API
- SQL Server
- Entity Framework Core (Code First & Fluent API)
- Dapper (for read-heavy queries)
- MediatR (CQRS Pattern)
- Redis (Caching)
- JWT Authentication
- Clean Architecture
- Swagger (API Documentation)

---

# 🏗 Architecture

The project follows **Clean Architecture** combined with **CQRS using MediatR**.

```
7adarny
│
├── 7adarny.Domain
│   ├── Entities
│   └── Enums
│
├── 7adarny.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Commands
│   ├── Queries
│   ├── Handlers
│   └── Services
│
├── 7adarny.Infrastructure
│   ├── Data
│   │   ├── DbContext
│   │   └── Configurations
│   ├── Repositories
│   └── Implementations
│
└── 7adarny.API
    ├── Controllers
    ├── Middleware
    └── Program.cs
```

---

# 🧠 CQRS Pattern

The project uses **CQRS (Command Query Responsibility Segregation)**.

This separates **write operations** from **read operations**.

### Commands (Write Operations)

Used for actions that **modify data**.

Examples:

- CreateGroupCommand
- EnrollStudentCommand
- MarkAttendanceCommand

Handled using **EF Core**.

---

### Queries (Read Operations)

Used for **retrieving data**.

Examples:

- GetTeacherGroupsQuery
- GetGroupStudentsQuery
- GetAttendanceReportQuery

Optimized using **Dapper** for better performance.

---

# 🔗 Relationships

```
Teacher
 ├── One-to-Many → Branch
 ├── One-to-Many → Grade
 ├── One-to-Many → Group
 ├── One-to-Many → Notification
 └── One-to-Many → Report

Group
 ├── Many-to-One → Teacher
 ├── Many-to-One → Branch
 ├── Many-to-One → Grade
 ├── One-to-Many → Enrollment
 └── One-to-Many → Attendance

Student
 ├── One-to-Many → Enrollment
 ├── One-to-Many → Attendance
 └── One-to-Many → OtpVerification
```

---

# ⚡ EF Core vs Dapper

The project combines **EF Core and Dapper**.

### EF Core
Used for:

- Commands
- CRUD operations
- Entity relationships
- Migrations

---

### Dapper
Used for:

- Read-heavy queries
- Statistics
- Reports
- Complex joins

---

# ⚡ Redis Caching Strategy

Redis is used to cache frequently accessed data.

| Key | Cached Data | Expiration |
|----|-------------|-----------|
| teacher:{id}:profile | Teacher profile | 1 hour |
| teacher:{id}:groups | Teacher groups | 30 min |
| group:{id}:students | Group students | 30 min |
| statistics:{id} | Group statistics | 15 min |

---

# 🔐 Authentication

The system uses **JWT Authentication**.

Teachers login using:

```
POST /api/auth/login
```

Students login using:

```
OTP verification
```

---

# 🛠 How to Run the Project

### 1️⃣ Clone Repository

```
git clone https://github.com/AmanyFarouk/7adarny.git
```

---

### 2️⃣ Update Connection String

In **appsettings.json**

---

### 3️⃣ Run the Project

```
dotnet run
```

Open Swagger:

```
https://localhost:5000/swagger
```

---

# 📌 Project Status

✔ Clean Architecture setup  
✔ Domain Entities implemented  
✔ EF Core Fluent Configurations added  

Next Steps:

⬜ CQRS Commands & Queries  
⬜ Authentication & Authorization  
⬜ Enrollment workflow  
⬜ Attendance system  
⬜ Redis caching  
⬜ Dapper reporting queries