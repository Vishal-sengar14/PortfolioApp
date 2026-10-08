using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>A single job / work experience entry shown on the Experience timeline.</summary>
    public class Experience
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Designation { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        /// <summary>Null means "currently working here".</summary>
        public DateTime? EndDate { get; set; }

        [Required]
        public string Responsibilities { get; set; } = string.Empty;

        [MaxLength(500)]
        public string TechnologiesUsed { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? CompanyLogoPath { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
