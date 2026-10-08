using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    /// <summary>A message submitted through the public Contact form and stored for the admin to read.</summary>
    public class Message
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required, MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public DateTime SentOn { get; set; } = DateTime.UtcNow;
    }
}
