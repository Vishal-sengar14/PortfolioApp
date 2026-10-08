using Microsoft.EntityFrameworkCore;
using PortfolioApp.Models;

namespace PortfolioApp.Data
{
    /// <summary>EF Core Code-First database context for the whole portfolio application.</summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectScreenshot> ProjectScreenshots => Set<ProjectScreenshot>();
        public DbSet<Experience> Experiences => Set<Experience>();
        public DbSet<Education> Educations => Set<Education>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Certificate> Certificates => Set<Certificate>();
        public DbSet<Testimonial> Testimonials => Set<Testimonial>();
        public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
        public DbSet<Setting> Settings => Set<Setting>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<Resume> Resumes => Set<Resume>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdminUser>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Setting>().HasIndex(s => s.Key).IsUnique();

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Screenshots)
                .WithOne(s => s.Project)
                .HasForeignKey(s => s.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Decimal / precision-sensitive columns none required here, but keep string lengths sane.
            modelBuilder.Entity<Skill>().Property(s => s.Category).HasConversion<string>();
            modelBuilder.Entity<Project>().Property(p => p.Status).HasConversion<string>();
        }
    }
}
