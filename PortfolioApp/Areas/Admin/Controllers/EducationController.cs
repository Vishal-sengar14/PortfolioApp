using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class EducationController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;

        public EducationController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _uow.Educations.GetAllAsync()).OrderBy(e => e.DisplayOrder).ToList();
            return View(list);
        }

        public IActionResult Create() => View(new Education());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education model)
        {
            if (!ModelState.IsValid) return View(model);
            await _uow.Educations.AddAsync(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Education added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _uow.Educations.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Education model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _uow.Educations.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Education updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _uow.Educations.GetByIdAsync(id);
            if (item != null)
            {
                _uow.Educations.Remove(item);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Education deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
