using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.ViewModels;

namespace PortfolioApp.Controllers
{
    public class SkillsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public SkillsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var skills = (await _uow.Skills.FindAsync(s => s.IsActive)).OrderBy(s => s.DisplayOrder).ToList();
            var vm = new SkillsViewModel { SkillsByCategory = skills.ToLookup(s => s.Category) };
            return View(vm);
        }
    }
}
