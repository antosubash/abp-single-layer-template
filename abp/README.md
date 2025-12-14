# ABP Framework Minimalist Startup Solution

## About this solution

This is a minimalist, non-layered startup solution with the ABP Framework. All the fundamental ABP modules are already installed.

## How to run

### Start PostgreSQL

The application needs to connect to a PostgreSQL database. You can start PostgreSQL using Docker Compose:

````bash
docker-compose up -d
````

This will start a PostgreSQL container with the following configuration:
- Database: `AbpTemplate`
- User: `postgres`
- Password: `postgres`
- Port: `5432`

### Run the application

After PostgreSQL is running, run the following command in the `AbpTemplate` directory:

````bash
dotnet run --migrate-database
````

This will create and seed the initial database. Then you can run the application with any IDE that supports .NET.

Happy coding..!
