# TheRestaurant API

A comprehensive restaurant reservation management system built with ASP.NET Core 8.0, featuring table management, reservation booking, menu management, and administrative controls with JWT authentication.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Data Models](#data-models)
- [API Endpoints](#api-endpoints)
- [Authentication & Security](#authentication--security)
- [Features](#features)
- [Development](#development)

## Overview

TheRestaurant is a restaurant management platform that enables:

- **Table Management**: Configure restaurant seating arrangements with capacity tracking
- **Reservation System**: Two-phase booking process with temporary holds and confirmed reservations
- **Menu Management**: Create, update, and manage menu items with pricing
- **User Authentication**: JWT-based authentication with refresh token support
- **Administrative Controls**: Role-based access for restaurant staff
- **Data Cleanup**: Automated background services for data maintenance
- **Availability Checking**: Real-time table availability with time slot management

## Architecture

### Technology Stack

- **Framework**: ASP.NET Core 8.0 with Controllers
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT Bearer tokens with refresh token support
- **Security**: BCrypt password hashing
- **Background Services**: Hosted services for automated cleanup
- **Documentation**: Swagger/OpenAPI integration
- **Logging**: Built-in ASP.NET Core logging
- **Validation**: Data Annotations with custom validation extensions

### Design Patterns

- **Repository Pattern**: Data access abstraction through repositories
- **Service Layer**: Business logic separated from controllers
- **Unit Type**: Functional programming pattern for void operations
- **Generic Response Pattern**: Standardized service responses with `ServiceResponse<T>`
- **Mapper Pattern**: Entity-DTO conversion with static mappers
- **Background Service Pattern**: Long-running tasks for data maintenance
- **Validation Pattern**: Custom validation with `IValidatable` interface

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB for development)
- JetBrains Rider, Visual Studio 2022 or VS Code

### Local Development Setup

1. **Clone the repository**
```bash
git clone <repository-url>
cd TheRestaurant
```

2. **Configure Database Connection**
   Update `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TheRestaurantDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
  }
}
```

3. **Configure JWT Settings**
```json
{
  "JwtSettings": {
    "Key": "this-is-a-very-secret-key-for-restaurant-admin-authentication-system",
    "Issuer": "TheRestaurant",
    "Audience": "TheRestaurant-Admin",
    "DurationInMinutes": 480
  }
}
```

4. **Run Database Migrations**
```bash
dotnet ef database update
```

5. **Start the Application**
```bash
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7011`
- HTTP: `http://localhost:5201`
- Swagger UI: `https://localhost:7011/swagger`

### Default Admin Account

The application seeds a default admin account:
- **Username**: `admin`
- **Password**: `admin123`

## Project Structure

```
TheRestaurant/
├── Controllers/              # API endpoint controllers
│   ├── AuthController.cs    # Authentication endpoints
│   ├── AvailabilityController.cs # Table availability checking
│   ├── MenuItemController.cs # Menu management
│   ├── ReservationsController.cs # Reservation management
│   ├── ReservationHoldsController.cs # Temporary holds
│   └── TablesController.cs  # Table configuration
├── Data/                    # Database context
│   └── TheRestaurantApiDbContext.cs
├── DTOs/                    # Data Transfer Objects
│   ├── AuthResponseDto.cs
│   ├── AvailabilityRequestDto.cs
│   ├── AvailabilityResponseDto.cs
│   ├── AvailabilityProcessorDto.cs
│   ├── LoginDto.cs
│   ├── MenuItemDto.cs
│   ├── MenuItemCreateDto.cs
│   ├── MenuItemUpdateDto.cs
│   ├── PersonalInfoDto.cs
│   ├── ReservationDto.cs
│   ├── ReservationCreateDto.cs
│   ├── TableDto.cs
│   └── ...
├── Enums/                   # Application enumerations
│   └── TimeSlot.cs          # Restaurant time slots
├── Mappers/                 # Entity-DTO mapping
│   ├── MenuItemMapper.cs
│   ├── ReservationMapper.cs
│   ├── ReservationHoldMapper.cs
│   ├── TableMapper.cs
│   └── ...
├── Models/                  # Entity Framework models
│   ├── MenuItem.cs
│   ├── Reservation.cs
│   ├── ReservationHold.cs
│   ├── RefreshToken.cs
│   ├── Table.cs
│   └── User.cs
├── Repositories/           # Data access layer
│   ├── IRepositories/      # Repository interfaces
│   │   ├── IMenuItemRepository.cs
│   │   ├── IReservationRepository.cs
│   │   ├── IReservationHoldRepository.cs
│   │   ├── IRefreshTokenRepository.cs
│   │   ├── ITableRepository.cs
│   │   └── IUserRepository.cs
│   ├── MenuItemRepository.cs
│   ├── ReservationRepository.cs
│   ├── ReservationHoldRepository.cs
│   ├── RefreshTokenRepository.cs
│   ├── TableRepository.cs
│   └── UserRepository.cs
├── Security/               # Security configurations
│   └── JwtSettings.cs
├── Services/               # Business logic layer
│   ├── IServices/          # Service interfaces
│   │   ├── IAuthService.cs
│   │   ├── IAvailabilityService.cs
│   │   ├── IMenuItemService.cs
│   │   ├── IReservationService.cs
│   │   ├── IReservationHoldService.cs
│   │   └── ITableService.cs
│   ├── AuthService.cs
│   ├── AvailabilityService.cs
│   ├── MenuItemService.cs
│   ├── ReservationService.cs
│   ├── ReservationHoldService.cs
│   ├── ReservationCleanupService.cs
│   └── TableService.cs
├── Utilities/              # Common utilities
│   ├── IUtilities/         # Utility interfaces
│   │   ├── IServiceResponse.cs
│   │   └── IValidatable.cs
│   ├── Generate.cs         # Action result generator
│   ├── ServiceResponse.cs  # Standardized responses
│   ├── TimeSlotExtensions.cs # Time slot utilities
│   ├── Unit.cs            # Functional unit type
│   └── ValidationExtensions.cs # Validation helpers
└── Migrations/             # EF Core migrations
```

## Data Models

### Core Entities

#### Table
```csharp
public class Table
{
    public int Number { get; set; }        // Primary key, manually assigned
    public int Capacity { get; set; }      // 1-10 people
    public virtual List<Reservation>? Reservations { get; set; }
}
```

#### Reservation
```csharp
public class Reservation
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public int TableNumber { get; set; }   // Foreign key to Table
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

#### ReservationHold
```csharp
public class ReservationHold
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public int TableNumber { get; set; }
    public int TableCapacity { get; set; }
}
```

#### MenuItem
```csharp
public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }       // Max 50 chars, unique
    public decimal Price { get; set; }     // Decimal(10,2)
    public string Description { get; set; } // Max 300 chars
    public string Image { get; set; }      // Max 500 chars (URL)
}
```

#### User
```csharp
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }   // Max 50 chars
    public string PasswordHash { get; set; } // BCrypt hash
    public DateTime CreatedAt { get; set; }
    public virtual List<RefreshToken>? RefreshTokens { get; set; }
}
```

#### RefreshToken
```csharp
public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }      // Max 128 chars
    public DateTime CreatedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }
    public int UserIdFk { get; set; }      // Foreign key to User
    public virtual User? User { get; set; }
}
```

### Time Slots

The restaurant operates with 4 two-hour time slots:

```csharp
public enum TimeSlot
{
    Slot10To12 = 1,  // 10:00 - 12:00
    Slot12To14 = 2,  // 12:00 - 14:00
    Slot14To16 = 3,  // 14:00 - 16:00
    Slot16To18 = 4   // 16:00 - 18:00
}
```

## API Endpoints

### Authentication (/api/auth)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/login` | Admin login | No |
| POST | `/api/auth/refresh` | Refresh access token | No |

#### Login Request
```json
{
  "userName": "admin",
  "password": "admin123"
}
```

#### Login Response
```json
{
  "statusCode": 200,
  "value": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "guid-string"
  },
  "message": "Login successful."
}
```

### Table Management (/api/tables)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/tables` | Get all tables | Yes |
| GET | `/api/tables/{table-number}` | Get specific table | Yes |
| POST | `/api/tables` | Create new table | Yes |
| DELETE | `/api/tables/{table-number}` | Delete table | Yes |

#### Create Table Request
```json
{
  "number": 1,
  "capacity": 4
}
```

### Availability Checking (/api/availability)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/availability` | Get available tables | No |

#### Availability Request
```json
{
  "partySize": 4,
  "date": "2025-08-30"
}
```

#### Availability Response
```json
{
  "statusCode": 200,
  "value": [
    {
      "date": "2025-08-30",
      "timeSlot": "10:00 - 12:00",
      "tableNumber": 5,
      "tableCapacity": 4
    }
  ],
  "message": "Available combinations of table + time slots fetched successfully."
}
```

### Reservation Holds (/api/reservation-holds)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/reservation-holds` | Get all held reservations | Yes |
| POST | `/api/reservation-holds` | Create reservation hold | No |

#### Create Hold Request
```json
{
  "date": "2025-08-30",
  "timeSlot": 1,
  "tableNumber": 5,
  "tableCapacity": 4
}
```

### Reservations (/api/reservations)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/reservations` | Get all reservations | Yes |
| GET | `/api/reservations/{reservation-email}` | Get reservations by email | Yes |
| POST | `/api/reservations` | Create reservation from hold | No |
| DELETE | `/api/reservations/{reservation-email}` | Cancel reservations | Yes |

#### Create Reservation Request
```json
{
  "reservationHoldId": 123,
  "personalInfo": {
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com"
  }
}
```

### Menu Items (/api/menu-items)

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/menu-items` | Get all menu items | No |
| POST | `/api/menu-items` | Create menu item | Yes |
| PATCH | `/api/menu-items/{id}` | Update menu item | Yes |

#### Create Menu Item Request
```json
{
  "name": "Grilled Salmon",
  "price": 24.99,
  "description": "Fresh Atlantic salmon grilled to perfection...",
  "image": "https://example.com/salmon.jpg"
}
```

#### Update Menu Item Request (PATCH)
```json
{
  "name": "Updated Salmon Dish",
  "price": 26.99,
  "description": null,
  "image": null
}
```

## Authentication & Security

### JWT Configuration

```json
{
  "JwtSettings": {
    "Key": "this-is-a-very-secret-key-for-restaurant-admin-authentication-system",
    "Issuer": "TheRestaurant",
    "Audience": "TheRestaurant-Admin",
    "DurationInMinutes": 480
  }
}
```

### Token Management

- **Access Tokens**: 8-hour duration (configurable)
- **Refresh Tokens**: 7-day duration
- **Token Revocation**: Refresh tokens can be revoked
- **Password Hashing**: BCrypt with automatic salt generation

### Security Headers

```csharp
// JWT Bearer Authentication
Authorization: Bearer <access-token>
```

## Features

### Two-Phase Booking System

1. **Hold Phase**: Customer selects available slot, system creates temporary hold
2. **Confirmation Phase**: Customer provides personal info, hold converts to reservation
3. **Cleanup**: Temporary holds expire automatically, confirmed reservations stored

### Real-Time Availability

- Filters past time slots for same-day bookings
- Excludes confirmed reservations and temporary holds
- Prioritizes smaller tables for optimal capacity utilization
- Handles concurrent booking attempts with unique constraints

### Administrative Features

- Table configuration management
- Reservation monitoring and management
- Menu item creation and updates
- Customer reservation lookup by email
- Bulk reservation cancellation

### Background Services

```csharp
// Automatic cleanup every 24 hours
public class ReservationCleanupService : BackgroundService
{
    // Removes reservations older than 6 months
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(24);
}
```

### Validation System

The application uses a custom validation system with:
- `IValidatable` interface for DTOs
- `ValidationExtensions` for automated validation
- Data Annotations for field-level validation
- Service-level business validation

## Development

### Adding New Features

1. **Create Models**: Define EF Core entities in `/Models`
2. **Create DTOs**: Define request/response DTOs in `/DTOs`
3. **Add Repositories**: Implement data access in `/Repositories`
4. **Implement Services**: Add business logic in `/Services`
5. **Create Controllers**: Define API endpoints in `/Controllers`
6. **Add Mappers**: Create entity-DTO conversion in `/Mappers`
7. **Update DbContext**: Register new entities
8. **Add Migrations**: Generate EF Core migrations

### Service Pattern Example

```csharp
public class MenuItemService : IMenuItemService
{
    public async Task<ServiceResponse<Unit>> CreateMenuItemAsync(MenuItemCreateDto dto)
    {
        return await dto.ValidateAndExecuteAsync(async () =>
        {
            try
            {
                var menuItem = MenuItemMapper.ToEntity(dto);
                await repository.CreateMenuItemAsync(menuItem);
                
                return ServiceResponse<Unit>.Success(
                    HttpStatusCode.Created,
                    Unit.Value,
                    "Menu item created successfully."
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating menu item");
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.InternalServerError,
                    "An error occurred while creating menu item."
                );
            }
        });
    }
}
```

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## Error Handling

### Standardized Responses

All endpoints return standardized responses:

```csharp
public record ServiceResponse<T>(
    HttpStatusCode StatusCode,
    T? Value,
    string Message
) : IServiceResponse
```

### Success Response
```json
{
  "statusCode": 200,
  "value": { /* data */ },
  "message": "Operation completed successfully."
}
```

### Error Response
```json
{
  "statusCode": 400,
  "value": null,
  "message": "Validation failed: Name is required."
}
```

### Validation Errors

- DTOs include validation attributes
- Complex validation in services with `ValidationExtensions`
- User-friendly error messages
- Proper HTTP status codes

## API Response Patterns

### Success Patterns
- `200 OK`: Successful retrieval
- `201 Created`: Successful creation
- `204 No Content`: Successful deletion

### Error Patterns
- `400 Bad Request`: Validation errors
- `401 Unauthorized`: Authentication required
- `403 Forbidden`: Access denied
- `404 Not Found`: Resource not found
- `409 Conflict`: Concurrent booking attempts
- `500 Internal Server Error`: Unexpected errors

## Contributing

1. Follow established architecture patterns
2. Add comprehensive tests for new features
3. Use proper validation on all inputs
4. Follow RESTful API conventions
5. Document new endpoints and features
6. Ensure proper error handling and logging
7. Test reservation flow end-to-end

## License

This project is developed for educational and demonstration purposes.