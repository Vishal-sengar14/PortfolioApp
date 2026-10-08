using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>Represents an administrator account that can log into the Admin Panel.</summary>
    public class AdminUser
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginOn { get; set; }
    }
}
