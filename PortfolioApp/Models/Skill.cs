using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.Models
{
    public enum SkillCategory
    {
        Backend,
        Frontend,
        Database,
        Tools,
        SoftSkills
    }

    /// <summary>A single technical or soft skill displayed with a progress bar on the Skills page.</summary>
    public class Skill
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100)]
        public int ProficiencyLevel { get; set; }

        /// <summary>Font Awesome icon class, e.g. "fa-brands fa-microsoft".</summary>
        [MaxLength(100)]
        public string IconClass { get; set; } = "fa-solid fa-code";

        public SkillCategory Category { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
