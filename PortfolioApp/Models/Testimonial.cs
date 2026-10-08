using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>A short client/colleague testimonial shown on the Home page.</summary>
    public class Testimonial
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string PersonName { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Designation { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? PhotoPath { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } = 5;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
