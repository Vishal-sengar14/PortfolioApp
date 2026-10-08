using PortfolioApp.Models;

namespace PortfolioApp.Data.Repositories.Interfaces
{
    /// <summary>Aggregates all repositories and exposes a single SaveChangesAsync so a controller
    /// commits one transaction per request (Unit of Work pattern on top of Repository pattern).</summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AdminUser> AdminUsers { get; }
        IGenericRepository<Skill> Skills { get; }
        IGenericRepository<Project> Projects { get; }
        IGenericRepository<ProjectScreenshot> ProjectScreenshots { get; }
        IGenericRepository<Experience> Experiences { get; }
        IGenericRepository<Education> Educations { get; }
        IGenericRepository<Message> Messages { get; }
        IGenericRepository<Certificate> Certificates { get; }
        IGenericRepository<Testimonial> Testimonials { get; }
        IGenericRepository<SocialLink> SocialLinks { get; }
        IGenericRepository<Setting> Settings { get; }
        IGenericRepository<Profile> Profiles { get; }
        IGenericRepository<Resume> Resumes { get; }

        Task<int> SaveChangesAsync();
    }
}
