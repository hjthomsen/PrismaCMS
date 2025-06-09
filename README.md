# PrismaCMS

A clean architecture financial management system for accounting firms, built with .NET 9.

## Project Overview

PrismaCMS is a customer management system designed for accounting firms to track financial statements, employee assignments, and time entries. The application follows Clean Architecture principles to ensure maintainable, testable code with clear separation of concerns.

## Architecture

The solution is organized into four layers:

### Domain Layer (PrismaCMS.Domain)

The core business entities, value objects, and business rules.

- **Entities**: Customer, FinancialStatement, Employee, Assignment, TimeEntry
- **Value Objects**: ContactInfo
- **Enums**: FinancialStatementStatus, EmployeeRole

### Application Layer (PrismaCMS.Application)

Application services, interfaces, and DTOs.

- **Interfaces**: IRepository<T>, IApplicationDbContext
- **Mapping**: AutoMapper profiles for entity-to-DTO transformations
- **DTOs**: CustomerDto, ContactInfoDto

### Infrastructure Layer (PrismaCMS.Infrastructure)

Implementation of persistence, external services, and other infrastructure concerns.

- **Database**: Entity Framework Core with SQL Server
- **Repositories**: Generic Repository implementation
- **Configuration**: Entity type configurations for EF Core
- **Seeding**: Sample data seeder

### API Layer (PrismaCMS.API)

REST API endpoints for client applications.

- **Controllers**: RESTful endpoints for CRUD operations
- **DTOs**: Request/response models
- **Configuration**: Application startup and DI setup

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (or SQL Server Express)

### Setup

1. Clone the repository

   ```
   git clone https://github.com/yourusername/PrismaCMS.git
   cd PrismaCMS
   ```

2. Update connection string in `appsettings.json`

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=PrismaCMSDb;User Id=sa;Password=YourPassword;TrustServerCertificate=true"
   }
   ```

3. Restore dependencies

   ```
   dotnet restore
   ```

4. Run migrations to create the database

   ```
   dotnet ef database update --project PrismaCMS.Infrastructure --startup-project PrismaCMS.API
   ```

5. Run the application
   ```
   cd PrismaCMS.API
   dotnet run
   ```

The application will automatically seed the database with sample data on first run.

## API Endpoints

### Customers

- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID
- `POST /api/customers` - Create a new customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

## Development

### Adding Migrations

```
dotnet ef migrations add MigrationName --project PrismaCMS.Infrastructure --startup-project PrismaCMS.API
```

### Updating Database

```
dotnet ef database update --project PrismaCMS.Infrastructure --startup-project PrismaCMS.API
```

## Clean Architecture Principles

This project follows Clean Architecture principles:

1. **Dependency Rule**: Dependencies only point inward. Outer layers can depend on inner layers, but inner layers cannot depend on outer layers.

2. **Domain Independence**: The domain layer has no dependencies on frameworks or external libraries.

3. **Separation of Concerns**: Each layer has a specific responsibility and shouldn't take on responsibilities from other layers.

4. **Boundary Crossing**: Data crossing boundaries is done using simple data structures (DTOs).

5. **Dependency Inversion**: High-level modules do not depend on low-level modules; both depend on abstractions.

## License

[MIT License](LICENSE)
