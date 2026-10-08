using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;

namespace PortfolioApp.Controllers
{
    public class EducationController : Controller
    {
        private readonly IUnitOfWork _uow;

        public EducationController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var educations = (await _uow.Educations.FindAsync(e => e.IsActive))
                .OrderByDescending(e => e.EndYear).ToList();
            return View(educations);
        }
    }
}
