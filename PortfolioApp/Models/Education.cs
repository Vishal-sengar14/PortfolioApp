using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>An academic qualification shown on the Education page/timeline.</summary>
    public class Education
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Degree { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Institution { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? CGPA { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
