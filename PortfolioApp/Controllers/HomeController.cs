using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;
using PortfolioApp.ViewModels;

namespace PortfolioApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _uow;

        public HomeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var profile = (await _uow.Profiles.GetAllAsync()).FirstOrDefault();
            ViewBag.TypedRoles = profile?.TypedRoles;
            var socialLinks = (await _uow.SocialLinks.FindAsync(s => s.IsActive))
                .OrderBy(s => s.DisplayOrder).ToList();
            var testimonials = (await _uow.Testimonials.FindAsync(t => t.IsActive))
                .OrderBy(t => t.DisplayOrder).ToList();
            var featuredProjects = (await _uow.Projects.FindAsync(p => p.IsActive && p.IsFeatured))
                .OrderBy(p => p.DisplayOrder).Take(3).ToList();

            var vm = new HomeViewModel
            {
                Profile = profile,
                SocialLinks = socialLinks,
                Testimonials = testimonials,
                FeaturedProjects = featuredProjects,
                ProjectsCount = await _uow.Projects.CountAsync(p => p.IsActive),
                SkillsCount = await _uow.Skills.CountAsync(s => s.IsActive),
                YearsOfExperience = profile?.YearsOfExperience ?? 0
            };

            return View(vm);
        }

        [ResponseCache(Duration = 3600)]
        public IActionResult Privacy() => View();

        public IActionResult Error404() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}
