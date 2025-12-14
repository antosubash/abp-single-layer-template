# ABP Single Layer Template

A minimalist, non-layered startup solution with the ABP Framework. This template provides a single-layer architecture (no separate Domain/Application/Infrastructure layers) for rapid development and simplicity, with practical customizations already applied.

## 🚀 Demo

- **Backend API**: <https://abp.antosubash.com>
- **React App**: <https://abpreact.antosubash.com>
- **Tanstack React App**: <https://abp-tanstack.antosubash.com>

## ✨ Features

This template includes several enhancements over the standard ABP no-layer template:

- ✅ **Improved Swagger**: Fixed class names for better readability
- ✅ **HTTPS Redirect**: Automatic HTTP to HTTPS redirection
- ✅ **Docker Support**: Production-ready Dockerfile included
- ✅ **Auto Migrations**: Database migrations run automatically on startup
- ✅ **SameSite Cookies**: Configured for better security
- ✅ **Database Data Protection**: Keys stored in the database
- ✅ **Seq Logging**: Integrated Serilog with Seq support
- ✅ **CI/CD Pipeline**: GitHub Actions workflow included
- ✅ **Versioning**: Project versioning configured

## 🛠️ Technology Stack

- **Framework**: .NET 10.0
- **ABP Framework**: 10.0.1
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Authentication**: OpenIddict
- **Logging**: Serilog (with Seq integration)
- **UI Theme**: LeptonXLite
- **Container**: Docker (Linux)

## 📋 Prerequisites

- .NET 10.0 SDK
- PostgreSQL (running locally or remotely)
- Docker (optional, for containerization)

## 🚀 Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/antosubash/abp-single-layer-template.git
cd abp-single-layer-template
```

### 2. Setup Database

Ensure PostgreSQL is running. The application expects:
- Database: `AbpTemplate` (or configure in `appsettings.json`)
- User: `postgres` (or your configured user)
- Password: `postgres` (or your configured password)
- Port: `5432`

> **Note**: PostgreSQL should be running on your system (not via Docker for development).

### 3. Configure Application

1. Update `appsettings.json` with your database connection string
2. Optionally configure `appsettings.secrets.json` for sensitive settings

### 4. Rename Project (Optional)

Search and replace `AbpTemplate` with your project name throughout the solution.

### 5. Run the Application

```bash
cd abp/AbpTemplate
dotnet run --migrate-database
```

The application will:
- Automatically create and run migrations
- Seed initial data
- Start on `https://localhost:44300` (or configured port)

### 6. Access the Application

- **Swagger UI**: `https://localhost:44300/swagger`
- **Web UI**: `https://localhost:44300`

Default credentials:
- Username: `admin`
- Password: `1q2w3E*`

## 📁 Project Structure

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

## 🔧 Development

### Running Migrations

Migrations run automatically on startup. To create a new migration:

```bash
cd abp/AbpTemplate
dotnet ef migrations add MigrationName
```

### Formatting Code

```bash
cd abp/AbpTemplate
dotnet csharpier .
```

### Adding Packages

```bash
cd abp/AbpTemplate
dotnet add package PackageName
```

## 📚 Documentation

- **For AI Agents**: See [AGENTS.md](AGENTS.md) for detailed guidelines, project structure, and coding conventions
- **ABP Framework**: <https://docs.abp.io>

## 🎯 Use Cases

This template is ideal for:
- Rapid prototyping
- Small to medium-sized applications
- Projects that don't require strict layer separation
- React + ABP backend projects
- Learning ABP Framework

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Commit Message Format

Use conventional commits:
- `fix:` for bug fixes
- `docs:` for documentation changes
- `refactor:` for code refactoring
- `chore:` for maintenance tasks

## 📝 License

This project is licensed under the [MIT License](LICENSE).

## 🙏 Acknowledgments

- [ABP Framework](https://abp.io) - The underlying framework
- [Volo ABP](https://github.com/abpframework/abp) - The open-source community

## 📞 Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Check the [ABP Framework documentation](https://docs.abp.io)
