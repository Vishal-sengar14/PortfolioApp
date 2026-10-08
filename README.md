# Vishal Sengar — Portfolio Website (ASP.NET Core MVC, .NET 10)

A complete, dynamic, admin-manageable portfolio website built with ASP.NET Core MVC (.NET 10), Entity
Framework Core (Code First), SQL Server, Repository + Unit of Work pattern, and a dark blue-gradient
glassmorphism UI (Bootstrap 5, AOS, Typed.js, SweetAlert2, Chart.js, Font Awesome).

## 1. Project Structure

```
PortfolioApp.sln
PortfolioApp/
 ├─ Areas/Admin/                 → Admin Panel (Controllers + Views, area-routed, cookie-auth protected)
 ├─ Controllers/                 → Public site controllers (Home, About, Skills, Projects, ...)
 ├─ Data/
 │   ├─ ApplicationDbContext.cs  → EF Core DbContext (Code First)
 │   ├─ DbInitializer.cs         → Seed data (your real info is pre-seeded)
 │   └─ Repositories/            → Generic Repository + Unit of Work pattern
 ├─ Models/                      → EF Core entities (Skill, Project, Experience, Education, Message, ...)
 ├─ Services/                    → IEmailService / EmailService (SMTP via MailKit)
 ├─ ViewModels/                  → Strongly typed view models
 ├─ Views/                       → Razor views for the public site
 ├─ wwwroot/                     → css/js/images, robots.txt, sitemap.xml
 ├─ Program.cs                   → App startup, DI, auth, middleware pipeline
 └─ appsettings.json             → Connection string + SMTP + Admin seed credentials
```

## 2. Prerequisites

- **Visual Studio 2026** (or later) with the **ASP.NET and web development** workload
- **.NET 10 SDK**
- **SQL Server** (LocalDB, SQL Express, or a full instance) + SQL Server Management Studio (optional)

## 3. First-Time Setup

### Step 1 — Open the project
Open `PortfolioApp.sln` in Visual Studio 2026.

### Step 2 — Configure your connection string & SMTP settings
Edit `PortfolioApp/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=PortfolioDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
},
"SmtpSettings": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "SenderEmail": "your-email@gmail.com",
  "Username": "your-email@gmail.com",
  "Password": "your-app-password",
  "AdminNotificationEmail": "your-email@gmail.com"
}
```
> For Gmail, generate an **App Password** (Google Account → Security → 2-Step Verification → App
> Passwords) — your normal Gmail password will NOT work with SMTP.

You can also change the default admin login here:
```json
"AdminSeed": { "Username": "admin", "Email": "admin@vishalsengar.dev", "Password": "Admin@123" }
```

### Step 3 — Restore NuGet packages
Visual Studio does this automatically on open. If needed, run in **Package Manager Console**:
```
dotnet restore
```

### Step 4 — Create the EF Core migration & database
Open **Package Manager Console** in Visual Studio (Tools → NuGet Package Manager → Package Manager
Console), set the **Default project** to `PortfolioApp`, then run:

```powershell
Add-Migration InitialCreate
Update-Database
```

Or, from a terminal in the `PortfolioApp/PortfolioApp` folder (with the `dotnet-ef` tool installed via
`dotnet tool install --global dotnet-ef`):

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This creates the `PortfolioDb` database with all tables (Users, Skills, Projects, Experiences,
Education, Messages, Certificates, Testimonials, SocialLinks, Settings, Profile, Resume) and the app
automatically seeds it with **Vishal Sengar's real information** on first run (see
`Data/DbInitializer.cs`).

### Step 5 — Run the project
Press **F5** (or `dotnet run` from the `PortfolioApp/PortfolioApp` folder). The app will:
1. Apply any pending migrations automatically (`context.Database.Migrate()` in `Program.cs`)
2. Seed initial data if the tables are empty
3. Launch the site at `https://localhost:xxxx`

### Step 6 — Log in to the Admin Panel
Go to `/Account/Login` and sign in with the credentials from `AdminSeed` in `appsettings.json`
(default: **admin** / **Admin@123**). From there you can manage Skills, Projects, Experience,
Education, Certificates, Testimonials, Social Links, Resume (PDF upload), Profile, and read Contact
Messages — all fully dynamic from SQL Server.

## 4. Notes & Extending

- **Repository Pattern**: `Data/Repositories/Interfaces/IGenericRepository.cs` +
  `IUnitOfWork.cs` are injected into every controller via constructor DI (`Program.cs` registers
  `IUnitOfWork` as scoped). Add a new entity by adding a `DbSet<T>` to `ApplicationDbContext`, a
  matching `IGenericRepository<T>` property to `IUnitOfWork`, and following the same CRUD controller
  pattern used for Skills/Projects/etc.
- **File uploads** (project images, certificates, testimonial photos, resume PDF) are saved under
  `wwwroot/images/...` and `wwwroot/uploads/resume/` via `Areas/Admin/Controllers/FileUploadHelperBase.cs`.
- **Authentication**: Cookie-based (`Microsoft.AspNetCore.Authentication.Cookies`), passwords hashed
  with BCrypt. The whole `Admin` area is protected with `[Authorize(Roles = "Admin")]`.
- **Logging**: Serilog writes rolling daily logs to `Logs/log-*.txt`.
- **SEO**: `wwwroot/robots.txt`, `wwwroot/sitemap.xml`, and meta/OG tags in `Views/Shared/_Layout.cshtml`.
- **Error handling**: custom 404 (`Views/Home/Error404.cshtml`) and 500 (`Views/Shared/Error.cshtml`)
  pages wired up in `Program.cs`.
- **Contact form**: stores messages in SQL Server (`Messages` table) and, if SMTP is configured, emails
  a notification to `AdminNotificationEmail` via MailKit.

## 5. Default Login

| Field | Value |
|---|---|
| URL | `/Account/Login` |
| Username | `admin` |
| Password | `Admin@123` |

**Change this password (or the seed values in `appsettings.json` before first run) before deploying
publicly.**

## 6. Tech Stack Recap

ASP.NET Core MVC (.NET 10) • C# • Entity Framework Core • SQL Server • Razor Views • Bootstrap 5 •
Font Awesome • AOS • Typed.js • SweetAlert2 • Chart.js • MailKit (SMTP) • Serilog • BCrypt.Net-Next •
Repository + Unit of Work pattern • Dependency Injection • Async/await throughout.
