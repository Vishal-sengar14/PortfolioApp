using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;

namespace PortfolioApp.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ExperienceController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var experiences = (await _uow.Experiences.FindAsync(e => e.IsActive))
                .OrderByDescending(e => e.StartDate).ToList();
            return View(experiences);
        }
    }
}
