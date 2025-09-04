<<<<<<< HEAD
# BrightLife IMS (Inventory Management System)

Inventory & asset management application built on ASP.NET Core 9.0 + Identity + EF Core. Ships with a lightweight default SQLite store (for frictionless local dev) and optional PostgreSQL + Redis infrastructure via Docker Compose.

## ✨ Highlights
* ASP.NET Core 9 + Identity UI
* EF Core (SQLite by default, PostgreSQL ready)
* Modular entity layer (extensible `CustomFields` per item)
* Redis (optional) for caching / future session state
* Containerized infra (Postgres, Redis, pgAdmin)

## 🛠 Prerequisites
* .NET 9.0 SDK
* Docker Desktop (only needed if you want Postgres/Redis)
* PowerShell / Bash / VS Code / Visual Studio 2022

## 🚀 Quick Start (Default: SQLite)
```powershell
git clone <repository-url>
cd BrightLifeIMS
dotnet restore .\BrightLifeIMS\src\BrightLifeIMS.Web\
cd .\BrightLifeIMS\src\BrightLifeIMS.Web\
dotnet ef database update   # Applies Identity schema to app.db
dotnet run
```
App URLs (development defaults):
* HTTPS: https://localhost:5001
* HTTP:  http://localhost:5000

SQLite connection string lives in `appsettings.json`:
```jsonc
"ConnectionStrings": {
  "DefaultConnection": "DataSource=app.db;Cache=Shared"
}
```

## 🐘 Switch to PostgreSQL + Redis (Optional)
1. Start infra:
   ```powershell
   docker-compose up -d
   ```
2. Replace the connection string (e.g. in `appsettings.Development.json` or via user secrets):
   ```jsonc
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=brightlife_ims;Username=brightlife_user;Password=brightlife_password"
   }
   ```
3. Add (or regenerate) migrations if schema changes:
   ```powershell
   dotnet ef migrations add InitialDomainSchema
   dotnet ef database update
   ```
4. (Optional) Add Redis configuration later (currently only infrastructure is provisioned).

> Tip: Keep both providers by using environment specific files; SQLite for quick local spikes, Postgres for integration tests.

## 🧱 Project Structure
```
BrightLifeIMS/
├── docker-compose.yml          # Postgres / Redis / pgAdmin
├── BrightLifeIMS/              # Solution root
│   ├── src/BrightLifeIMS.Web/  # Web + MVC/Identity/EF
│   │   ├── Data/               # DbContext & (future) repositories
│   │   ├── Models/             # Entities / DTOs / ViewModels
│   │   ├── Components/         # UI components
│   │   ├── Controllers/        # MVC + future API controllers
│   │   ├── Services/           # Business logic layers (placeholder)
│   │   └── wwwroot/            # Static assets
│   └── tests/                  # Unit + integration test projects
└── docs/ (future documentation)
```

## 📦 Key Packages
| Purpose | Package |
|---------|---------|
| Identity & EF Core | Microsoft.AspNetCore.Identity.* / EntityFrameworkCore.* |
| Data Providers | Sqlite, Npgsql (PostgreSQL) |
| Tooling | Microsoft.EntityFrameworkCore.Tools / Design |
| Caching | StackExchange.Redis extension |
| Media | CloudinaryDotNet |
| Auth Providers | Google, Facebook |
| Data Import | CsvHelper |
| JSON | Newtonsoft.Json |

## 🗃 Entity Framework Cheat Sheet
Create migration:
```powershell
dotnet ef migrations add <Name>
```
Apply migrations:
```powershell
dotnet ef database update
```
List migrations:
```powershell
dotnet ef migrations list
```

Common mistake (don’t chain accidentally):
```
dotnet ef migrations add InitialSchemadotnet ef database update  # ❌
```
Instead run sequentially or separate with `;` in PowerShell:
```powershell
dotnet ef migrations add InitialSchema; dotnet ef database update
```

## 🔐 Identity
Configured with `RequireConfirmedAccount = true` by default. To relax for local dev you can modify options in `Program.cs` or seed test users (seeding helper to be added).

## 🧩 Extensibility (Custom Item Fields)
`Item` entity includes a `Dictionary<string, object> CustomFields` for ad‑hoc attributes. Recommended persistence approaches later:
1. JSON column (PostgreSQL `jsonb`) using EF Core value conversion.
2. Separate key/value table (if querying keys frequently).

Currently (SQLite) it is not mapped; add a converter when ready.

## 🧪 Testing
Projects:
* `BrightLifeIMS.UnitTests`
* `BrightLifeIMS.IntegrationTests`

Run all tests from solution root:
```powershell
dotnet test
```

## 🌱 Environment & Secrets
Use User Secrets for local sensitive config:
```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=..."
```

## 🐳 Helpful Docker Commands
```powershell
docker-compose up -d        # start services
docker-compose down         # stop & remove containers
docker-compose logs -f postgres
```

## 🔄 Roadmap (Near Term)
1. Define domain entities (Inventory, Tag, User extensions)
2. Map `CustomFields` via JSON / separate table
3. Introduce caching layer abstraction
4. Add API endpoints (REST) for inventory items
5. Implement seeding + sample data
6. CI workflow (build + test + migrations lint)

## 🤝 Contributing
1. Fork & branch (`feature/<short-name>`)
2. Keep changes small & tested
3. Run `dotnet format` (if added later) before PR
4. Open PR with context & screenshots (UI changes)

## 📚 References
* ASP.NET Core: https://learn.microsoft.com/aspnet/core
* EF Core: https://learn.microsoft.com/ef/core
* PostgreSQL: https://www.postgresql.org/docs/
* Redis: https://redis.io/docs

## 📄 License
MIT License (add LICENSE file if not present).

---
Need a section added (CI, deployment, seeding)? Open an issue or request it.
