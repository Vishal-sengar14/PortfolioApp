using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class SkillsController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;

        public SkillsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var skills = (await _uow.Skills.GetAllAsync()).OrderBy(s => s.Category).ThenBy(s => s.DisplayOrder).ToList();
            return View(skills);
        }

        public IActionResult Create() => View(new Skill());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill model)
        {
            if (!ModelState.IsValid) return View(model);

            await _uow.Skills.AddAsync(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Skill created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _uow.Skills.GetByIdAsync(id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Skill model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _uow.Skills.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Skill updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var skill = await _uow.Skills.GetByIdAsync(id);
            if (skill != null)
            {
                _uow.Skills.Remove(skill);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Skill deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
