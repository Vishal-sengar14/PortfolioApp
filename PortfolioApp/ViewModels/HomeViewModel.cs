using PortfolioApp.Models;

namespace PortfolioApp.ViewModels
{
    public class HomeViewModel
    {
        public Profile? Profile { get; set; }
        public List<SocialLink> SocialLinks { get; set; } = new();
        public List<Testimonial> Testimonials { get; set; } = new();
        public List<Project> FeaturedProjects { get; set; } = new();

        public int ProjectsCount { get; set; }
        public int SkillsCount { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
