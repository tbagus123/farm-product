# Farm product Readme

# Prerequisites

•	.NET 6.0 SDK
•	Visual Studio 2022 or Visual Studio Code
•	SQL Server

# Configuration
Clone the resporitory
git clone tbagus123/farm-product (github.com)
cd MyFarmProduct
Set Up Connection String: Ensure your appsettings.json file has the correct connection string for your SQL Server database:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyFarmProductDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}


# Code explanation

•  Configure Services:

•	AddDbContext: Registers ApplicationDbContext with a SQL Server provider using a connection string from configuration.

•	AddDefaultIdentity: Sets up ASP.NET Core Identity with default settings, including required account confirmation and role management.

•  Build Application:

•	Environment Configuration: Configures the HTTP request pipeline for development and production environments.

•	Middleware: Configures essential middleware components like HTTPS redirection, static file serving, routing, authentication, and authorization.

•  Database Seeding:

•	CreateScope: Creates a scope to obtain services and initialize the database with seed data using DataSeed.Initialize.

•  Routing:

•	MapControllerRoute: Sets up the default route for MVC controllers.

•	MapRazorPages: Maps Razor Pages endpoints.

# Running the application
1)	Apply Migrations
Ensure database is updated with latest schema changes.


2)	Run the application
Start the application using the .net cli or your preferred ide

       3)Access the application
Open web browser and and go to https://localhost:5001

