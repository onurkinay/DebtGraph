# DebtGraph

DebtGraph is a .NET 9.0 web application for managing and visualizing customer debts and invoices. It uses Entity Framework Core for data access and provides a web interface for interacting with customer and invoice data.

## Features
- Manage customer and invoice records
- Visualize debt relationships
- RESTful API endpoints
- MVC web interface
- SQL scripts for database setup

## Project Structure
- `Controllers/` - API and MVC controllers
- `Models/` - Data models (e.g., Debt, Request)
- `Context/` and `Data/` - Entity Framework Core DbContext classes
- `Migrations/` - EF Core migration files
- `SQL/` - SQL scripts for database tables
- `Views/` - Razor views for the web UI
- `wwwroot/` - Static files (CSS, JS, images)
- `appsettings.json` - Application configuration

## Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server or compatible database

### Setup
1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd DebtGraph
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. Open your browser at `https://localhost:5001` (or the port specified in `launchSettings.json`).

### Database
- SQL scripts for initial tables are in the `SQL/` directory.
- Entity Framework Core migrations are in the `Migrations/` directory.

## Configuration
- Edit `appsettings.json` or `appsettings.Development.json` to configure the database connection string and other settings.

## License
Specify your license here.

## Contact
For questions or support, contact the project maintainer.
