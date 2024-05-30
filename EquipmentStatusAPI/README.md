# Equipment Status API

## Overview
This is a simplified .NET Core application that serves as a backend for an equipment status dashboard. It provides functionalities to manage and track the status of various equipment, making it ideal for real-time status monitoring and updates.

## Setup Instructions
1. **Clone the repository**: Clone the code to your local machine.
2. **Navigate to the project directory**: Change into the project's directory on your machine.
3. **Install dependencies**: Run `dotnet restore` to restore all the necessary .NET packages.
4. **Database setup**: Execute `dotnet ef database update` to apply migrations and set up the SQLite database.
5. **Run the application**: Use `dotnet run` to start the application. The API will be available on `http://localhost:<port>` given in running terminal.

## Database Choice
SQLite is employed as the database for this application due to its simplicity and ease of setup, which is ideal for small projects. It facilitates rapid development without the need for complex database configuration.

## Architectural Pattern
The application is structured following the MVC (Model-View-Controller) pattern, ensuring a clear separation of concerns between the user interface, data handling, and business logic. 

This pattern enhances the maintainability and scalability of the application.

## Models
The application defines an `EquipmentStatus` model which includes:
- `Id` (int): A primary surrogate key for database efficiency.
- `EquipmentId` (string): A non-unique name identifier for the equipment.
- `Status` (string): Describes the current status of the equipment.
- `UpdateDate` (DateTime): Timestamp for the last status update.

## Database Configuration
The database is configured in `Program.cs` using Entity Framework Core with the following setup:
```csharp
builder.Services.AddDbContext<EquipmentStatusContext>(options =>
    options.UseSqlite("Data Source=equipmentstatus.db"));
```

## API Endpoints
The `EquipmentStatusController` includes various endpoints:
- **POST `/status`**: Create a new status entry.
- **GET `/status`**: List all statuses.
- **GET `/status/{equipmentId}`**: Fetch the status for a pieces of equipment with name "equipmentId".
- **PUT `/status/{Id}`**: Update an existing status entry - added for debugging purposes.
- **DELETE `/status/{Id}`**: Delete a status entry - added for debugging purposes.


## Unit/Integration Testing

The `EquipmentStatusAPI.Tests` project conducts integration tests using an in-memory database to validate the functionality and reliability of the main API endpoints.

- **POST `/status` Tests**:
  - **Valid Data**: Confirms that submitting correct status data creates a new status entry, indicated by a `CreatedAtActionResult`.
  - **Invalid Data**: Verifies that submitting null data results in a `BadRequestObjectResult`, ensuring the API's error handling is effective.
  
- **GET `/status/{equipmentId}` Tests**:
  - **Valid ID**: Tests that querying a valid ID retrieves the correct status, returning an `OkObjectResult`.
  - **Invalid ID**: Ensures that querying a non-existent ID results in a `NotFoundObjectResult`, important for maintaining API usability.

Each test operates in a fully isolated environment, using a fresh instance of the `EquipmentStatusContext` for every test case. 
This setup mirrors actual database operations, providing a realistic and controlled testing environment.

## Swagger Documentation
Swagger is integrated to document the API and provide an interactive interface for testing the endpoints.

### How to Access the API Documentation
Once the application is running, you can access the Swagger UI to view and test the API endpoints by navigating to:
```
http://localhost:<port>/api-docs
```

The generated API documentation will be available and accessible through the Swagger UI, making it easy to understand and test the API.

---

