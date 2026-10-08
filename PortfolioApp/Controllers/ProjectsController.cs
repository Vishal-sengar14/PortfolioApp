using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.ViewModels;

namespace PortfolioApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ProjectsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index(string? tech, string? search, int page = 1)
        {
            var all = (await _uow.Projects.FindAsync(p => p.IsActive)).OrderBy(p => p.DisplayOrder).ToList();

            var allTechnologies = all
                .SelectMany(p => p.TechStack.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            var filtered = all.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(tech))
            {
                filtered = filtered.Where(p => p.TechStack.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Contains(tech, StringComparer.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                filtered = filtered.Where(p =>
                    p.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.ShortDescription.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            var filteredList = filtered.ToList();
            const int pageSize = 6;
            var totalItems = filteredList.Count;

            var paged = filteredList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new ProjectListViewModel
            {
                Projects = paged,
                AllTechnologies = allTechnologies,
                SelectedTechnology = tech,
                SearchTerm = search,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _uow.Projects.Query()
                .Include(p => p.Screenshots)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (project == null) return NotFound();

            return View(project);
        }
    }
}
