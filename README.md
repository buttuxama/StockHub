# StockHub

StockHub is a .NET 8 Web API for stock tracking with user authentication, stock and comment management, and per-user portfolio management.

## What is implemented

- JWT authentication with ASP.NET Core Identity
- Account registration and login endpoints
- Role seeding for `Admin` and `User`
- Stock CRUD endpoints
- Stock querying with filtering, sorting, and pagination
- Comment CRUD endpoints linked to stocks
- Portfolio management (add/remove stocks, list current user portfolio)
- Repository pattern with interfaces and implementations
- DTO and mapper layers for request/response shaping
- SQL Server integration with Entity Framework Core
- Swagger/OpenAPI with Bearer token support
- SQL project (`Stockhub.sqlproj`) that builds a DACPAC

## Tech stack

- Runtime: .NET 8 (`net8.0`)
- Framework: ASP.NET Core Web API
- ORM: Entity Framework Core 8
- Database: SQL Server
- Auth: ASP.NET Core Identity + JWT Bearer
- API docs/testing: Swagger (Swashbuckle)
- JSON handling: Newtonsoft.Json
- SQL schema project: Microsoft.Build.Sql (`Stockhub.sqlproj`)

## Main packages

- `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.24)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (8.0.24)
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.24)
- `Microsoft.EntityFrameworkCore.Design` (8.0.24)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.24)
- `Microsoft.AspNetCore.Mvc.NewtonsoftJson` (8.0.24)
- `Swashbuckle.AspNetCore` (6.6.2)

## Solution structure

- `api/`: ASP.NET Core Web API project
  - `Controller/`: API endpoints
  - `Model/`: Domain models (`Stock`, `Comment`, `Portfolio`, `AppUser`)
  - `Dto/`: Request and response DTOs
  - `Mapper/`: Mapping extensions between models and DTOs
  - `Repository/`: Data access implementations
  - `Interface/`: Repository and service contracts
  - `Data/`: EF Core DbContext
  - `Service/`: Token generation service
  - `Migrations/`: EF Core migrations
- `Stockhub/`: SQL project that builds database artifacts (DACPAC)
- `StockHub.sln`: Solution file

## Implemented API endpoints

Base URL (development): `http://localhost:5257` or `https://localhost:7031`

### Account

- `POST /api/account/register` - Create user and return JWT
- `POST /api/account/login` - Authenticate user and return JWT

### Stocks (Authorize required)

- `GET /api/stock` - List stocks with query options
- `GET /api/stock/{id}` - Get stock by id
- `POST /api/stock` - Create stock
- `PUT /api/stock/{id}` - Update stock
- `DELETE /api/stock/{id}` - Delete stock

Supported stock query parameters:

- `Symbol`
- `CompanyName`
- `SortBy` (`Symbol`, `CompanyName`, `Purchase`, `LastDividend`, `Industry`, `MarketCap`)
- `isDescending` (`true`/`false`)
- `PageNumber` (default `1`)
- `PageSize` (default `10`)

### Comments (Authorize required)

- `GET /api/comment` - List comments
- `GET /api/comment/{id}` - Get comment by id
- `POST /api/comment/{stockId}` - Create comment for a stock
- `PUT /api/comment/{id}` - Update comment
- `DELETE /api/comment/{id}` - Delete comment

### Portfolio (Authorize required)

- `GET /api/portfolio` - Get current user's portfolio
- `POST /api/portfolio?symbol={symbol}` - Add stock to current user's portfolio
- `DELETE /api/portfolio?symbol={symbol}` - Remove stock from current user's portfolio

## Data model summary

- `Stock`
  - Core fields: symbol, company name, purchase, last dividend, industry, market cap
  - One-to-many with `Comment`
  - Many-to-many with users through `Portfolio`
- `Comment`
  - Title, content, created timestamp, optional stock reference
- `AppUser`
  - Identity user extended with portfolio navigation
- `Portfolio`
  - Join entity: composite key (`UserId`, `StockId`)

## Database and migrations

The API project includes EF Core migrations for:

- Initial schema creation
- ASP.NET Core Identity tables
- Role seeding
- Portfolio many-to-many relationship

Role seeding includes:

- `Admin`
- `User`

## Configuration

`api/appsettings.json` currently defines:

- `ConnectionStrings:DefaultConnection`
- `JWT:Issuer`
- `JWT:Audience`
- `JWT:SigningKey`

Update the database credentials to match your local SQL Server setup before running.

## Run locally

### Prerequisites

- .NET SDK 8+
- SQL Server (local or remote)

### Steps

1. Restore dependencies:

```bash
dotnet restore
```

2. Update `api/appsettings.json` with valid SQL Server credentials.

3. Apply migrations (from `api/`):

```bash
dotnet ef database update
```

4. Run the API (from `api/`):

```bash
dotnet run
```

5. Open Swagger:

- `http://localhost:5257/swagger`
- or `https://localhost:7031/swagger`

## Authentication flow

1. Register or login via account endpoints.
2. Copy the returned JWT token.
3. In Swagger, click `Authorize` and use:

```text
Bearer <your-token>
```

4. Call protected endpoints.

## SQL project

The `Stockhub/Stockhub.sqlproj` project is configured with `Microsoft.Build.Sql` and can be built to produce a DACPAC artifact.

Example build command:

```bash
dotnet build Stockhub/Stockhub.sqlproj
```

## Current status

Implemented core backend capabilities include:

- Identity + JWT authentication
- Stock, comment, and portfolio APIs
- EF Core persistence and migrations
- Swagger-based API exploration
- SQL project scaffolding for schema artifact generation