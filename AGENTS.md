# AI Agent Guidelines

This document provides context and guidelines for AI agents working with this ABP Framework template project.

## Project Overview

This is a minimalist, non-layered ABP Framework startup solution template. It's designed as a single-layer architecture (no separate Domain/Application/Infrastructure layers) for rapid development and simplicity.

## Technology Stack

- **Framework**: .NET 10.0
- **ABP Framework**: 10.0.1
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Authentication**: OpenIddict
- **Logging**: Serilog (with Seq integration)
- **UI Theme**: LeptonXLite
- **Container**: Docker (Linux)

## Project Structure

```
abp/AbpTemplate/
├── Controllers/          # API Controllers
├── Data/                # DbContext and migration services
├── Entities/            # Domain entities
├── Extensions/          # Extension methods and configurations
├── Localization/        # Localization resources
├── Migrations/          # EF Core migrations
├── ObjectMapping/       # AutoMapper profiles
├── Permission/          # Permission definitions
├── Repository/          # Custom repositories
├── Services/            # Application services and DTOs
└── Utils/               # Utility classes and constants
```

## Key Files

- `Program.cs`: Application entry point with Serilog configuration and automatic migrations
- `AbpTemplateModule.cs`: Main ABP module configuration
- `AbpTemplateDbContext.cs`: EF Core DbContext
- `Utils/Constant.cs`: String constants (avoid hardcoding strings)
- `appsettings.json`: Application configuration
- `Dockerfile`: Container configuration

## Coding Conventions

### General Rules

1. **No Hardcoded Strings**: Always use constants from `Utils/Constant.cs` or localization resources
2. **File Size**: Keep files under 500 lines - split large files into smaller ones
3. **Simple Implementation**: Prioritize simple solutions over complex ones
4. **Package Management**: Never add packages directly to `.csproj` files - always use `dotnet add package` CLI
5. **Code Formatting**: Use `dotnet csharpier .` to format code after modifications
6. **Linting**: Always lint and format modified files after writing code

### ABP Framework Conventions

- Follow ABP Framework naming conventions
- Use dependency injection for all services
- Implement proper permission checks
- Use AutoMapper for object mapping
- Follow ABP's multi-tenancy patterns

### Database

- Migrations run automatically on startup (configured in `Program.cs`)
- Use `AbpTemplateDbMigrationService` for migration management
- PostgreSQL is assumed to be running (not in Docker for development)

## Common Tasks

### Adding a New Entity

1. Create entity class in `Entities/` folder
2. Add DbSet to `AbpTemplateDbContext`
3. Create migration: `dotnet ef migrations add MigrationName`
4. Migration will run automatically on next startup

### Adding a New Controller

1. Create controller in `Controllers/` folder
2. Inherit from `AbpControllerBase` or appropriate ABP base class
3. Add proper authorization attributes
4. Use dependency injection for services

### Adding a New Service

1. Create service class in `Services/` folder
2. Implement appropriate interface
3. Register in `AbpTemplateModule.cs` if needed
4. Use dependency injection

### Adding Constants

1. Add to `Utils/Constant.cs` as public const string
2. Use the constant instead of hardcoded strings

### Adding Packages

```bash
cd abp/AbpTemplate
dotnet add package PackageName
```

## Development Workflow

1. **Database Setup**: PostgreSQL should already be running (not via Docker)
2. **Run Application**: 
   ```bash
   cd abp/AbpTemplate
   dotnet run --migrate-database
   ```
3. **Format Code**: 
   ```bash
   dotnet csharpier .
   ```
4. **Create Migrations**: 
   ```bash
   dotnet ef migrations add MigrationName
   ```

## Important Notes

- **Migrations**: Run automatically on startup - no manual migration needed
- **Secrets**: Use `appsettings.secrets.json` for sensitive configuration
- **Logging**: Logs are written to `Logs/logs.txt` and optionally to Seq
- **Multi-tenancy**: Template supports multi-tenancy via ABP Framework
- **Data Protection**: Keys are stored in the database
- **HTTPS**: HTTP redirects to HTTPS are configured
- **Swagger**: Available at `/swagger` with improved class names

## Commit Messages

Use conventional commits format:
- `fix:` for bug fixes
- `docs:` for documentation changes
- `refactor:` for code refactoring
- `chore:` for maintenance tasks
- Avoid overusing `feat:` prefix

## Testing

- Ensure code compiles without errors
- Verify migrations work correctly
- Test with multi-tenant scenarios if applicable
- Check Swagger documentation is accessible

## Resources

- ABP Framework Documentation: https://docs.abp.io
- Demo: https://abp.antosubash.com
- React App Demo: https://abpreact.antosubash.com

