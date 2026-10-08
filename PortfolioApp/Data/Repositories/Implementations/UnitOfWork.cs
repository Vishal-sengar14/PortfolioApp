using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Data.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<AdminUser> AdminUsers { get; }
        public IGenericRepository<Skill> Skills { get; }
        public IGenericRepository<Project> Projects { get; }
        public IGenericRepository<ProjectScreenshot> ProjectScreenshots { get; }
        public IGenericRepository<Experience> Experiences { get; }
        public IGenericRepository<Education> Educations { get; }
        public IGenericRepository<Message> Messages { get; }
        public IGenericRepository<Certificate> Certificates { get; }
        public IGenericRepository<Testimonial> Testimonials { get; }
        public IGenericRepository<SocialLink> SocialLinks { get; }
        public IGenericRepository<Setting> Settings { get; }
        public IGenericRepository<Profile> Profiles { get; }
        public IGenericRepository<Resume> Resumes { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            AdminUsers = new GenericRepository<AdminUser>(context);
            Skills = new GenericRepository<Skill>(context);
            Projects = new GenericRepository<Project>(context);
            ProjectScreenshots = new GenericRepository<ProjectScreenshot>(context);
            Experiences = new GenericRepository<Experience>(context);
            Educations = new GenericRepository<Education>(context);
            Messages = new GenericRepository<Message>(context);
            Certificates = new GenericRepository<Certificate>(context);
            Testimonials = new GenericRepository<Testimonial>(context);
            SocialLinks = new GenericRepository<SocialLink>(context);
            Settings = new GenericRepository<Setting>(context);
            Profiles = new GenericRepository<Profile>(context);
            Resumes = new GenericRepository<Resume>(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
