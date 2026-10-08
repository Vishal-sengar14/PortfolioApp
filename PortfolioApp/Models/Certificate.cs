using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>A professional certificate/award displayed in the About/Achievements section.</summary>
    public class Certificate
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? IssuedBy { get; set; }

        public DateTime IssuedOn { get; set; }

        [MaxLength(300)]
        public string? ImagePath { get; set; }

        [MaxLength(300)]
        public string? CredentialUrl { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
