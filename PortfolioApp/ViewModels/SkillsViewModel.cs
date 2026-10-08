using PortfolioApp.Models;

namespace PortfolioApp.ViewModels
{
    public class SkillsViewModel
    {
        public ILookup<SkillCategory, Skill> SkillsByCategory { get; set; } = default!;
    }
}
