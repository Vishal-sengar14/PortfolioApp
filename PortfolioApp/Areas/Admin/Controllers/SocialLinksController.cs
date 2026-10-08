using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class SocialLinksController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;

        public SocialLinksController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _uow.SocialLinks.GetAllAsync()).OrderBy(s => s.DisplayOrder).ToList();
            return View(list);
        }

        public IActionResult Create() => View(new SocialLink());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialLink model)
        {
            if (!ModelState.IsValid) return View(model);
            await _uow.SocialLinks.AddAsync(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Social link added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _uow.SocialLinks.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialLink model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _uow.SocialLinks.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Social link updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _uow.SocialLinks.GetByIdAsync(id);
            if (item != null)
            {
                _uow.SocialLinks.Remove(item);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Social link deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
