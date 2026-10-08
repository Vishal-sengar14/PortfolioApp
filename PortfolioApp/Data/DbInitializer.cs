using PortfolioApp.Models;

namespace PortfolioApp.Data
{
    /// <summary>Seeds the database with the site owner's real data on first run so the site
    /// is populated the moment migrations are applied.</summary>
    public static class DbInitializer
    {
        /// <summary>Seeds initial data. Call AFTER context.Database.Migrate() has been run in Program.cs.</summary>
        public static void Seed(ApplicationDbContext context, IConfiguration config)
        {
            // ---- Admin user ----
            if (!context.AdminUsers.Any())
            {
                var seedSection = config.GetSection("AdminSeed");
                var username = seedSection["Username"] ?? "admin";
                var email = seedSection["Email"] ?? "admin@example.com";
                var password = seedSection["Password"] ?? "Admin@123";

                context.AdminUsers.Add(new AdminUser
                {
                    Username = username,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    IsActive = true
                });
            }

            // ---- Profile ----
            if (!context.Profiles.Any())
            {
                context.Profiles.Add(new Profile
                {
                    FullName = "Vishal Sengar",
                    Title = "Software Developer",
                    TypedRoles = ".NET Developer,ASP.NET Core Developer,Full Stack Developer,C# Programmer",
                    Bio = "Software Developer with 2 years of experience building scalable, data-driven web " +
                          "applications using ASP.NET Core, C#, Entity Framework Core, and SQL Server. " +
                          "Currently working at Vatsal Healthcare Solutions Pvt. Ltd., focused on writing " +
                          "clean, maintainable code and delivering reliable healthcare software solutions.",
                    CareerObjective = "To grow as a full-stack .NET developer, contributing to impactful " +
                          "products while continuously improving my skills in modern web architecture, " +
                          "cloud technologies, and software design principles.",
                    Email = "vishal.sengar@example.com",
                    Phone = "+91-00000-00000",
                    Address = "India",
                    YearsOfExperience = 2
                });
            }

            // ---- Skills ----
            if (!context.Skills.Any())
            {
                context.Skills.AddRange(
                    new Skill { Name = "ASP.NET Core", ProficiencyLevel = 85, Category = SkillCategory.Backend, IconClass = "fa-brands fa-microsoft", DisplayOrder = 1 },
                    new Skill { Name = ".NET", ProficiencyLevel = 85, Category = SkillCategory.Backend, IconClass = "fa-solid fa-layer-group", DisplayOrder = 2 },
                    new Skill { Name = "C#", ProficiencyLevel = 85, Category = SkillCategory.Backend, IconClass = "fa-solid fa-code", DisplayOrder = 3 },
                    new Skill { Name = "SQL Server", ProficiencyLevel = 80, Category = SkillCategory.Database, IconClass = "fa-solid fa-database", DisplayOrder = 4 },
                    new Skill { Name = "Entity Framework Core", ProficiencyLevel = 80, Category = SkillCategory.Backend, IconClass = "fa-solid fa-diagram-project", DisplayOrder = 5 },
                    new Skill { Name = "HTML5", ProficiencyLevel = 90, Category = SkillCategory.Frontend, IconClass = "fa-brands fa-html5", DisplayOrder = 6 },
                    new Skill { Name = "CSS3", ProficiencyLevel = 85, Category = SkillCategory.Frontend, IconClass = "fa-brands fa-css3-alt", DisplayOrder = 7 },
                    new Skill { Name = "Bootstrap", ProficiencyLevel = 85, Category = SkillCategory.Frontend, IconClass = "fa-brands fa-bootstrap", DisplayOrder = 8 },
                    new Skill { Name = "JavaScript", ProficiencyLevel = 75, Category = SkillCategory.Frontend, IconClass = "fa-brands fa-js", DisplayOrder = 9 },
                    new Skill { Name = "jQuery", ProficiencyLevel = 75, Category = SkillCategory.Frontend, IconClass = "fa-solid fa-code", DisplayOrder = 10 },
                    new Skill { Name = "Git", ProficiencyLevel = 80, Category = SkillCategory.Tools, IconClass = "fa-brands fa-git-alt", DisplayOrder = 11 },
                    new Skill { Name = "GitHub", ProficiencyLevel = 80, Category = SkillCategory.Tools, IconClass = "fa-brands fa-github", DisplayOrder = 12 },
                    new Skill { Name = "Problem Solving", ProficiencyLevel = 85, Category = SkillCategory.SoftSkills, IconClass = "fa-solid fa-puzzle-piece", DisplayOrder = 13 },
                    new Skill { Name = "Team Collaboration", ProficiencyLevel = 85, Category = SkillCategory.SoftSkills, IconClass = "fa-solid fa-people-group", DisplayOrder = 14 }
                );
            }

            // ---- Experience ----
            if (!context.Experiences.Any())
            {
                context.Experiences.Add(new Experience
                {
                    CompanyName = "Vatsal Healthcare Solutions Pvt. Ltd.",
                    Designation = "Software Developer",
                    StartDate = DateTime.UtcNow.AddYears(-2),
                    EndDate = null,
                    Responsibilities = "Design and develop healthcare web applications using ASP.NET Core MVC.\n" +
                                        "Build and maintain SQL Server databases with Entity Framework Core.\n" +
                                        "Implement authentication, reporting, and PDF export features.\n" +
                                        "Collaborate with cross-functional teams to deliver reliable, maintainable software.",
                    TechnologiesUsed = "ASP.NET Core, C#, SQL Server, Entity Framework Core, JavaScript, Bootstrap",
                    DisplayOrder = 1
                });
            }

            // ---- Education ----
            if (!context.Educations.Any())
            {
                context.Educations.Add(new Education
                {
                    Degree = "Bachelor of Technology (B.Tech)",
                    Institution = "College / University",
                    CGPA = "7.3",
                    StartYear = DateTime.UtcNow.Year - 6,
                    EndYear = DateTime.UtcNow.Year - 2,
                    Description = "Completed a B.Tech degree with a strong foundation in computer science, " +
                                   "programming, databases, and software engineering fundamentals.",
                    DisplayOrder = 1
                });
            }

            // ---- Project ----
            if (!context.Projects.Any())
            {
                context.Projects.Add(new Project
                {
                    Title = "Annual Exam List Web Application",
                    ShortDescription = "A healthcare web application that manages annual patient examination records.",
                    FullDescription = "Developed a healthcare web application that manages annual patient examination " +
                                       "records. The system supports patient management, advanced search and filtering, " +
                                       "full CRUD operations, SQL Server integration, authentication, report generation, " +
                                       "PDF export, and a fully responsive UI for use across devices.",
                    TechStack = "ASP.NET Core,C#,SQL Server,Entity Framework Core,Bootstrap,JavaScript",
                    Features = "Patient management\nSearch and filtering\nCRUD operations\nAuthentication\n" +
                               "Report generation\nPDF export\nResponsive UI",
                    Status = ProjectStatus.Completed,
                    IsFeatured = true,
                    DisplayOrder = 1
                });
            }

            // ---- Social Links ----
            if (!context.SocialLinks.Any())
            {
                context.SocialLinks.AddRange(
                    new SocialLink { PlatformName = "GitHub", IconClass = "fa-brands fa-github", Url = "https://github.com/", DisplayOrder = 1 },
                    new SocialLink { PlatformName = "LinkedIn", IconClass = "fa-brands fa-linkedin", Url = "https://linkedin.com/", DisplayOrder = 2 },
                    new SocialLink { PlatformName = "Twitter", IconClass = "fa-brands fa-x-twitter", Url = "https://twitter.com/", DisplayOrder = 3 },
                    new SocialLink { PlatformName = "Email", IconClass = "fa-solid fa-envelope", Url = "mailto:vishal.sengar@example.com", DisplayOrder = 4 }
                );
            }

            // ---- Settings ----
            if (!context.Settings.Any())
            {
                context.Settings.AddRange(
                    new Setting { Key = "SiteTitle", Value = "Vishal Sengar | Software Developer" },
                    new Setting { Key = "MetaDescription", Value = "Portfolio of Vishal Sengar, a .NET Full Stack Software Developer." }
                );
            }

            context.SaveChanges();
        }
    }
}
