using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    public enum ProjectStatus
    {
        Completed,
        InProgress,
        Planned
    }

    /// <summary>A portfolio project shown on the Projects page, loaded dynamically from SQL Server.</summary>
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ShortDescription { get; set; } = string.Empty;

        public string? FullDescription { get; set; }

        [MaxLength(300)]
        public string? ImagePath { get; set; }

        /// <summary>Comma separated technology stack, e.g. "ASP.NET Core,SQL Server,EF Core".</summary>
        [MaxLength(500)]
        public string TechStack { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? GitHubUrl { get; set; }

        [MaxLength(300)]
        public string? LiveDemoUrl { get; set; }

        /// <summary>Newline separated list of key features.</summary>
        public string? Features { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Completed;

        public int DisplayOrder { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<ProjectScreenshot> Screenshots { get; set; } = new List<ProjectScreenshot>();
    }

    /// <summary>Additional screenshots belonging to a project (one-to-many).</summary>
    public class ProjectScreenshot
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        [Required, MaxLength(300)]
        public string ImagePath { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Caption { get; set; }
    }
}
