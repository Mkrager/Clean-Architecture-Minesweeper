# Minesweeper Game Application

A full-featured Minesweeper game application built with Clean Architecture, utilizing SignalR for real-time gameplay updates. It includes an automated solver service and a leaderboard system with multiple difficulty levels.

## Features

- Real-time gameplay updates using SignalR.
- Automated Minesweeper solver service that plays for you.
- Leaderboards for each difficulty level: Easy, Medium, Hard.
- Clear separation of concerns using Clean Architecture and Mediator/CQRS patterns.

## Technology Stack

- **Frontend:** JavaScript, HTML, CSS
- **Backend:** ASP.NET Core 8, C#, Entity Framework Core
- **Database:** Microsoft SQL Server (MSSQL)
- **Architecture:** Clean Architecture with layered structure (API, Domain, Application, Persistence, Infrastructure, UI)
- **Real-time:** SignalR for live game updates

## Architecture Layers

- **API Layer:** Handles HTTP endpoints and SignalR hubs for real-time communication.
- **Domain Layer:** Core game entities.
- **Application Layer:** Implements CQRS with MediatR handlers.
- **Persistence Layer:** Manages data storage and access using Entity Framework Core.
- **Infrastructure Layer:** Integrations like SignalR, game and solver implementation.
- **UI Layer:** Frontend components with dynamic updates for gameplay.

## How to Run the Project Locally

### Getting Started

1. Clone the repository to your local machine.

```bash
git clone https://github.com/Mkrager/Clean-Architecture-Minesweeper.git
```

2. Set up your development environment. Make sure you have the necessary tools and packages installed.

3. Configure the project settings and dependencies. You may need to create configuration files for sensitive information like API keys and database connection strings.

4. Install the required packages using your package manager of choice (e.g., npm, yarn, NuGet).

5. Run the application locally for development and testing.

```bash
dotnet run
```

