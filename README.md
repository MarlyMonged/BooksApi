# Books API 📚

A RESTful Web API built with **ASP.NET Core** for managing books, authors, and publishers. The project implements CRUD operations, authentication and authorization, DTO-based data transfer, repository and service layers, and centralized logging.

The API is designed with a layered architecture to maintain separation of concerns, improve maintainability, and provide a clean structure for extending the application.

---

## ✨ Features

* 📚 Manage books with full **CRUD operations**
* ✍️ Manage authors
* 🏢 Manage publishers
* 🔐 User authentication using **ASP.NET Core Identity**
* 🎫 **JWT Bearer Authentication**
* 🛡️ Authentication and authorization for protected endpoints
* 🔄 DTOs for separating API contracts from database entities
* 🗂️ Repository Pattern for data access abstraction
* ⚙️ Service Layer for business logic
* 🔀 AutoMapper for Entity ↔ DTO mapping
* 🗄️ Entity Framework Core with SQL Server
* 🧩 Dependency Injection
* 🛠️ EF Core Migrations
* 📝 Centralized logging using **Serilog**
* 📖 Interactive API documentation using **Swagger / OpenAPI**
* ❗ Custom exception handling structure
* 🧱 Separation of concerns through interfaces and layered components

---

## 🏗️ Architecture

The project follows a layered architecture that separates responsibilities between controllers, services, repositories, and data access.

```text
BooksApi
│
├── Controllers
│   ├── AuthController
│   ├── BooksController
│   ├── AuthorsController
│   └── PublishersController
│
├── Dtos
│   ├── Authentication
│   ├── Author
│   ├── Book
│   ├── Publisher
│   └── ...
│
├── Data
│   └── ApplicationDbContext
│
├── Models
│
├── Repositories
│
├── Services
│
├── Interfaces
│
├── Exceptions
│
├── Migrations
│
├── ViewModels
│
├── MappingProfile.cs
│
└── Program.cs
```

---

## 🔄 Request Flow

A typical API request follows this flow:

```text
Client
  │
  ▼
Controller
  │
  ▼
Service Layer
  │
  ▼
Repository
  │
  ▼
Entity Framework Core
  │
  ▼
SQL Server
```

This separation keeps the controllers focused on handling HTTP requests while business logic and data access remain isolated in their respective layers.

---

## 📚 Main Resources

### Books

The API provides endpoints for managing books, including creating, retrieving, updating, and deleting book records.

```text
GET     /api/books
GET     /api/books/{id}
POST    /api/books
PUT     /api/books/{id}
DELETE  /api/books/{id}
```

### Authors

Authors can be created, retrieved, updated, and deleted through dedicated API endpoints.

```text
GET     /api/authors
GET     /api/authors/{id}
POST    /api/authors
PUT     /api/authors/{id}
DELETE  /api/authors/{id}
```

### Publishers

Publishers are managed through their own controller and service layer.

```text
GET     /api/publishers
GET     /api/publishers/{id}
POST    /api/publishers
PUT     /api/publishers/{id}
DELETE  /api/publishers/{id}
```

> Endpoint routes may vary depending on the controller routing configuration.

---

## 🔐 Authentication & Authorization

The API uses **ASP.NET Core Identity** together with **JWT Bearer Authentication**.

Authentication flow:

```text
User
 │
 ▼
Authentication Endpoint
 │
 ▼
Validate Credentials
 │
 ▼
Generate JWT
 │
 ▼
Client
 │
 ▼
Authorization Header
 │
 ▼
Protected API Endpoint
```

Authenticated requests use a Bearer token:

```http
Authorization: Bearer <JWT_TOKEN>
```

This allows protected endpoints to verify the identity and authorization of the requesting user.

---

## 🧩 DTOs & AutoMapper

The API uses **Data Transfer Objects (DTOs)** instead of exposing database entities directly through API endpoints.

Example flow:

```text
Database Entity
      ↓
   AutoMapper
      ↓
     DTO
      ↓
   API Response
```

DTOs are organized by domain, including:

* Authentication
* Books
* Authors
* Publishers

There is also a dedicated DTO for returning books together with author names.

---

## 🗂️ Repository & Service Layers

The application separates business logic and data access using interfaces, services, and repositories.

### Repository Layer

Responsible for communicating with the database through Entity Framework Core.

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
DbContext
    ↓
SQL Server
```

### Service Layer

Responsible for application/business logic and coordinating operations between controllers and repositories.

This structure helps keep controllers lightweight and makes the application easier to maintain and extend.

---

## 🗄️ Database

The project uses:

* **SQL Server**
* **Entity Framework Core**
* **Code First Migrations**

EF Core is responsible for:

* Database access
* Entity relationships
* CRUD operations
* Migrations
* Data persistence

---

## 📝 Logging

The application uses **Serilog** for structured application logging.

Logging can be configured to write to:

* File
* SQL Server

This provides a centralized way to monitor application activity and troubleshoot errors.

---

## 🛠️ Technologies

| Technology                    | Usage                   |
| ----------------------------- | ----------------------- |
| **C#**                        | Programming language    |
| **.NET 10**                   | Application framework   |
| **ASP.NET Core Web API**      | RESTful API             |
| **Entity Framework Core**     | ORM / Data Access       |
| **SQL Server**                | Database                |
| **ASP.NET Core Identity**     | User management         |
| **JWT Bearer Authentication** | Authentication          |
| **AutoMapper**                | Object mapping          |
| **Repository Pattern**        | Data access abstraction |
| **Service Layer**             | Business logic          |
| **Dependency Injection**      | Dependency management   |
| **Serilog**                   | Application logging     |
| **Swagger / OpenAPI**         | API documentation       |
| **Git & GitHub**              | Version control         |

---

## 📁 Project Structure

```text
BooksApi
│
├── Controllers
│   ├── AuthController.cs
│   ├── AuthorsController.cs
│   ├── BooksController.cs
│   └── PublishersController.cs
│
├── Data
│
├── Dtos
│   ├── Authentication
│   ├── Author
│   ├── Book
│   ├── Publisher
│   └── BookWithAuthorNamesDto.cs
│
├── Exceptions
│
├── Interfaces
│
├── Migrations
│
├── Models
│
├── Repositories
│
├── Services
│
├── ViewModels
│
├── MappingProfile.cs
├── Program.cs
└── BooksApi.csproj
```

---

## ⚙️ Getting Started

### Prerequisites

Make sure you have installed:

* **.NET 10 SDK**
* **SQL Server**
* **Visual Studio 2022/2026** or **VS Code**
* **Git**

---

### 1. Clone the Repository

```bash
git clone https://github.com/MarlyMonged/BooksApi.git
cd BooksApi
```

---

### 2. Configure the Database

Update the SQL Server connection string in:

```text
appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

Replace `YOUR_CONNECTION_STRING` with your SQL Server connection string.

---

### 3. Apply EF Core Migrations

Run:

```bash
dotnet ef database update
```

This applies the existing migrations and creates/updates the database.

---

### 4. Run the Application

Run the project using Visual Studio or:

```bash
dotnet run
```

---

## 📖 API Documentation

After running the application, open the Swagger UI to explore and test the available endpoints.

Swagger provides:

* Available API endpoints
* HTTP methods
* Request parameters
* Request/response models
* Authentication testing
* Interactive endpoint execution

---

## 🧪 Testing the API

You can test the API using:

* Swagger UI
* Postman
* `.http` request files
* Any REST API client

Typical workflow:

```text
1. Register/Login
       ↓
2. Receive JWT Token
       ↓
3. Authorize Swagger/Postman
       ↓
4. Create Author
       ↓
5. Create Publisher
       ↓
6. Create Book
       ↓
7. Retrieve / Update / Delete Resources
```

---

## 🎯 Project Goals

This project was built to practice and demonstrate practical experience with:

* Building RESTful APIs with ASP.NET Core
* CRUD operations
* Entity Framework Core
* SQL Server
* Authentication and Authorization
* JWT Authentication
* ASP.NET Core Identity
* DTOs and object mapping
* Repository Pattern
* Service Layer
* Dependency Injection
* Exception handling
* Structured logging
* API documentation with Swagger
* Layered architecture and separation of concerns

---

## 🚀 Future Improvements

Possible future enhancements include:

* 🧪 Unit and integration testing
* 📄 Pagination and filtering
* 🔎 Advanced search functionality
* 📊 API performance optimization
* 🚦 Rate limiting
* 📦 Caching
* 🐳 Docker support
* 🔄 CI/CD pipeline
* 📚 More advanced book/author relationships

---

## 👨‍💻 Author

**Marly Monged**

.NET Developer focused on building backend applications using:

**C# · ASP.NET Core · Entity Framework Core · SQL Server · REST APIs**

---

## 🔗 Repository

[GitHub Repository](https://github.com/MarlyMonged/BooksApi)
