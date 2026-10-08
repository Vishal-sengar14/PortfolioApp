using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.ViewModels;

namespace PortfolioApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _uow;

        public AboutController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AboutViewModel
            {
                Profile = (await _uow.Profiles.GetAllAsync()).FirstOrDefault(),
                Experiences = (await _uow.Experiences.FindAsync(e => e.IsActive)).OrderBy(e => e.DisplayOrder).ToList(),
                Educations = (await _uow.Educations.FindAsync(e => e.IsActive)).OrderBy(e => e.DisplayOrder).ToList(),
                Certificates = (await _uow.Certificates.FindAsync(c => c.IsActive)).OrderBy(c => c.DisplayOrder).ToList()
            };
            return View(vm);
        }
    }
}
