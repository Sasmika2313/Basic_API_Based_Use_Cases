# Basic API-Based Use Cases

A collection of beginner-friendly REST API projects built using **C#, ASP.NET Core Web API, Entity Framework Core, and SQL Server**.

This repository contains two simple API projects developed to practise backend development, database connectivity, and CRUD operations.

## Projects Included

### 1. Food Menu API

A REST API for managing food menu items.

**Features**
- Retrieve food items.
- Add new food items.
- Update existing food items.
- Delete food items.
- Store and manage data using SQL Server.

**Technologies:** C#, ASP.NET Core Web API, Entity Framework Core, SQL Server.

### 2. Movie Catalogue API

A REST API for managing a collection of movies.

**Features**
- Retrieve all movies.
- Search movies by title.
- Filter movies by genre.
- Retrieve a movie by ID.
- Add new movies.
- Update movie details.
- Delete movies.
- Validate incoming request data.

**Technologies:** C#, ASP.NET Core Web API, Entity Framework Core, SQL Server, Swagger, Postman.

## Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core Web API (.NET 10)
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Database Management:** SQL Server Management Studio (SSMS)
- **API Testing:** Postman and Swagger
- **IDE:** Visual Studio
- **Version Control:** Git and GitHub

## Repository Structure

```text
Basic_API_Based_Use_Cases/
│
├── HCL1_Demo_Web_Application/
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   └── ...
│
├── HCL1_Movie_Catalogue_API/
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   └── ...
│
├── .gitignore
└── README.md
```

## Getting Started

### Prerequisites

Install the following tools:

- [Visual Studio](https://visualstudio.microsoft.com/)
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/en-in/sql-server/sql-server-downloads)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/ssms/install/install)

Postman is optional but useful for testing API endpoints.

### Setup Instructions

1. Clone this repository:

   ```bash
   git clone https://github.com/Sasmika2313/Basic_API_Based_Use_Cases.git
   ```

2. Open either project folder in Visual Studio and open its `.csproj` file.

3. Configure the SQL Server connection string in `appsettings.json` to match your local SQL Server instance.

4. Open **Tools → NuGet Package Manager → Package Manager Console** in Visual Studio.

5. Select the appropriate project and run the Entity Framework Core migration commands if migrations are included:

   ```powershell
   Update-Database
   ```

   If the project has no migration yet, create one first:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

6. Run the project from Visual Studio.

7. Use Swagger or Postman to send requests to the API endpoints.

> **Note:** Configure and run each project separately. Database setup and migration commands may differ depending on the existing project configuration.

## Learning Outcomes

Through these projects, I practised:

- Building RESTful APIs using ASP.NET Core.
- Implementing CRUD operations.
- Connecting applications to SQL Server.
- Using Entity Framework Core for database operations.
- Testing API endpoints using Swagger and Postman.
- Managing source code using Git and GitHub.

## Future Improvements

- Add authentication and authorization.
- Implement pagination and sorting.
- Improve error handling and validation.
- Add unit tests and API documentation.
- Deploy the APIs to a cloud platform.

## Author

**Sasmika M**

GitHub: [@Sasmika2313](https://github.com/Sasmika2313)

---

This repository is a learning project focused on understanding the fundamentals of backend development and API design.
