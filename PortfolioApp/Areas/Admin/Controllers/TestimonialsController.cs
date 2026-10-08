using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class TestimonialsController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public TestimonialsController(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _uow.Testimonials.GetAllAsync()).OrderBy(t => t.DisplayOrder).ToList();
            return View(list);
        }

        public IActionResult Create() => View(new Testimonial());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial model, IFormFile? photoFile)
        {
            if (!ModelState.IsValid) return View(model);

            var path = await FileUploadHelper.SaveFileAsync(photoFile, _env, "images/profile");
            if (path != null) model.PhotoPath = path;

            await _uow.Testimonials.AddAsync(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Testimonial added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _uow.Testimonials.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Testimonial model, IFormFile? photoFile)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var path = await FileUploadHelper.SaveFileAsync(photoFile, _env, "images/profile");
            if (path != null) model.PhotoPath = path;

            _uow.Testimonials.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Testimonial updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _uow.Testimonials.GetByIdAsync(id);
            if (item != null)
            {
                _uow.Testimonials.Remove(item);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Testimonial deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
