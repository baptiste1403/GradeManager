# GradeManager

GradeManager is a .NET Web API designed to streamline student grade management. It implements a secure, role-based access control system for Administrators, Teachers, and Students. I use this project to learn features of the .net framework.

## Features

- **Secure Authentication**: Built-in JWT-based authentication and authorization.
- **Role-Based Access Control (RBAC)**:
  - **Admin**: Management of user registration (Teachers and Students).
  - **Teacher**: Manage assigned students, and record grades.
  - **Student**: View personal academic grades.
- **Data Persistence**: Utilizes SQLite for data storage with Entity Framework Core.
- **API Documentation**: Integrated Swagger/OpenAPI support for interactive API exploration.

## Tech Stack

- **Framework**: .NET 10
- **Database**: SQLite with EF Core (Lazy Loading enabled)
- **Security**: ASP.NET Core Identity & JWT Bearer Authentication
- **Documentation**: Swagger/OpenAPI

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation & Setup

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd GradeManager
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```
   The application will automatically create the SQLite database (`grade_manager.db`) and seed the initial identity data.

### Default Admin Credentials
For initial setup and testing, use the following administrator account:
- **Email**: `admin@admin.fr`
- **Password**: `SecureAdminPassword123!`

## API Overview

### Authentication (`/api/auth`)
- `POST /login`: Generates a JWT token for valid credentials.
- `POST /register-student`: (Admin only) Registers a new Student user.
- `POST /register-teacher`: (Admin only) Registers a new Teacher user.

### Teacher Actions (`/api/Teacher` & `/api/Student`)
- `GET /api/Teacher/students`: Returns students assigned to the logged-in teacher.
- `POST /api/Teacher/assign-student`: Links a student to the teacher's roster.
- `POST /api/Student/add-grade`: Allows teachers to submit grades for their students.

### Student Actions (`/api/Student`)
- `GET /api/Student/grades`: Returns the grade history for the authenticated student.
