using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>A social/profile link (GitHub, LinkedIn, etc.) shown as an animated icon on the Home page.</summary>
    public class SocialLink
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string PlatformName { get; set; } = string.Empty;

        /// <summary>Font Awesome icon class, e.g. "fa-brands fa-github".</summary>
        [Required, MaxLength(100)]
        public string IconClass { get; set; } = string.Empty;

        [Required, MaxLength(300)]
        public string Url { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
