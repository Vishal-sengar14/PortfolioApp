using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>Singleton-style record holding the owner's core profile/introduction information.</summary>
    public class Profile
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        /// <summary>Comma separated list of rotating titles used by Typed.js, e.g. ".NET Developer,Backend Engineer".</summary>
        [MaxLength(300)]
        public string TypedRoles { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string? CareerObjective { get; set; }

        [MaxLength(300)]
        public string? ProfileImagePath { get; set; }

        [MaxLength(150), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        public int YearsOfExperience { get; set; }
    }
}
