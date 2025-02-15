# ConcertCleanArchitecture

**ConcertCleanArchitecture** is a concert reservation application developed using ASP.NET Core, following Clean Architecture principles. The project emphasizes layered architecture to ensure code readability, maintainability, and testability.

## Project Structure

The project is divided into the following main layers:

- **Domain**: Contains the core business logic and entities.
- **Application**: Includes services, interfaces, and business logic-related operations.
- **Infrastructure**: Handles data access, external service integrations, and other infrastructure details.
- **Web**: Represents the presentation layer, including APIs and user interface components.

## Getting Started

To set up and run the project locally, follow these steps:

1. **Clone the Repository**: Clone the project to your local machine.
   ```bash
   git clone https://github.com/CevdetTufan/ConcertCleanArchitecture.git
   ```

2. **Restore Dependencies**: Navigate to the project directory and restore the necessary dependencies.
   ```bash
   cd ConcertCleanArchitecture
   dotnet restore
   ```

3. **Apply Database Migrations**: Use Entity Framework Core to apply the database migrations.
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/Web
   ```

4. **Run the Application**: Start the application by running the following command:
   ```bash
   dotnet run --project src/Web
   ```

## Contributing

If you would like to contribute, please open an issue first to discuss your proposed changes. Once your idea is approved, you can make your changes and submit a pull request.

## License

This project is licensed under the MIT License. For more details, please refer to the `LICENSE` file.


