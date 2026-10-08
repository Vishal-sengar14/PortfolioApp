using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class ProjectsController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public ProjectsController(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var projects = (await _uow.Projects.GetAllAsync()).OrderBy(p => p.DisplayOrder).ToList();
            return View(projects);
        }

        public IActionResult Create() => View(new Project());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model, IFormFile? imageFile, List<IFormFile>? screenshotFiles)
        {
            if (!ModelState.IsValid) return View(model);

            var imagePath = await FileUploadHelper.SaveFileAsync(imageFile, _env, "images/projects");
            if (imagePath != null) model.ImagePath = imagePath;

            await _uow.Projects.AddAsync(model);
            await _uow.SaveChangesAsync();

            if (screenshotFiles != null)
            {
                foreach (var file in screenshotFiles)
                {
                    var path = await FileUploadHelper.SaveFileAsync(file, _env, "images/projects");
                    if (path != null)
                    {
                        await _uow.ProjectScreenshots.AddAsync(new ProjectScreenshot { ProjectId = model.Id, ImagePath = path });
                    }
                }
                await _uow.SaveChangesAsync();
            }

            TempData["Success"] = "Project created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _uow.Projects.Query().Include(p => p.Screenshots).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project model, IFormFile? imageFile)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var imagePath = await FileUploadHelper.SaveFileAsync(imageFile, _env, "images/projects");
            if (imagePath != null) model.ImagePath = imagePath;

            _uow.Projects.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Project updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _uow.Projects.GetByIdAsync(id);
            if (project != null)
            {
                _uow.Projects.Remove(project);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Project deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
