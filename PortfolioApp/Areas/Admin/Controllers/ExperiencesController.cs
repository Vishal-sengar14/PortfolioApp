using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class ExperiencesController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;

        public ExperiencesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _uow.Experiences.GetAllAsync()).OrderBy(e => e.DisplayOrder).ToList();
            return View(list);
        }

        public IActionResult Create() => View(new Experience());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Experience model)
        {
            if (!ModelState.IsValid) return View(model);
            await _uow.Experiences.AddAsync(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Experience added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _uow.Experiences.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Experience model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _uow.Experiences.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Experience updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _uow.Experiences.GetByIdAsync(id);
            if (item != null)
            {
                _uow.Experiences.Remove(item);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Experience deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
