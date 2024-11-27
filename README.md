<h1 align="center" style="font-weight: bold;">Tech Trader API </h1>

<p align="center">
<a href="#technologies">Technologies</a>
<a href="#started">Getting Started</a>
<a href="#postman-documentation"> Postman Documentation</a>
<a href="#links"> Additional Project Links</a>
</p>

<p align="center">This is the server-side repo for the full-stack app Tech Trader — a marketplace app for tech lovers.</p>

<h2 id="technologies">Technologies</h2>

- C#
- .NET
- Entity Framework Core
- Moq
- xUnit
- PostgreSQL
- pgAdmin
- Swagger
- Postman

<h2 id="started">Getting started</h2>

<h3>Prerequisites</h3>

For this project to run successfully, you'll need the following:

- [.NET](https://dotnet.microsoft.com/en-us)
- [PostgreSQL](https://www.postgresql.org/download)
- [pgAdmin](https://www.pgadmin.org)

<h3>1. Clone the Repo</h3>

Clone this repo using this command in your terminal:

```bash
git clone https://github.com/jessefrench/Tech-Trader-Server.git
```

<h3>2. Install Required Packages</h3>

Once the repository is cloned, navigate to the project directory in your terminal and run the following commands to install necessary packages:

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 6.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0
```

<h3>3. Set Up Secrets for PostgreSQL Connection</h3>

To store sensitive connection details, initialize the secret storage with:

```bash
dotnet user-secrets init
```

Then, set the connection string for your PostgreSQL database (replace with your actual PostgreSQL password):

```bash
dotnet user-secrets set "TechTraderDbConnectionString" "Host=localhost;Port=5432;Username=postgres;Password=<your_postgresql_password>;Database=TechTrader"
```

<h3>4. Apply Migrations to the Database</h3>

Run the following command to apply the database migrations:

```bash
dotnet ef database update
```

This will create the necessary tables and schema in your PostgreSQL database.

<h3>5. Run the Solution</h3>

Launch the solution. Swagger should automatically launch and provide you with the API documentation.

<h2 id="postman-documentation">Postman Documentation</h2>

Check out the [Tech Trader Postman Documentation](https://www.postman.com/jessefrench/tech-trader/documentation/jyjd2id/tech-trader-api?workspaceId=393023e8-f3e8-4bb0-b06a-5028a8c25f35) to learn more about the API routes and see examples of how each request and response should look.

<h2 id="links">Additional Project Links</h2>

- [ERD](https://dbdiagram.io/d/Tech-Trader-6733af0ae9daa85aca375e54)
- [Frontend Wireframe](https://www.figma.com/design/dnXNNcrtKGU1yyD63IfcTv/Tech-Trader-Wireframe?node-id=0-1&t=Qne7gCatYCAMntuQ-1)
