# CoSpace

CoSpace is a multi-tenant web API developed using .NET Core C#. It is designed to manage and organize working spaces across various organizations or teams. Built with Clean Architecture and Mediatr, CoSpace ensures a scalable, maintainable, and testable codebase, providing robust features to support multiple tenants.

## Features

- **Multi-Tenant Support**: Efficiently manage multiple organizations or teams within a single instance of the API.
- **Clean Architecture**: Ensures separation of concerns, making the API highly maintainable and scalable.
- **Mediatr Integration**: Facilitates handling requests and commands, leading to cleaner, more organized code.
- **Secure API**: Implements best practices for authentication and authorization.
- **Extensible**: Easily add new features or modify existing ones without disrupting the overall structure.

## Technologies Used

- **.NET Core C#**: The main framework used for development.
- **Entity Framework Core**: For database interactions and migrations.
- **Mediatr**: For handling CQRS (Command Query Responsibility Segregation) patterns.
- **SQL Server**: Default database for storing application data.
- **Swagger**: For API documentation and testing.
