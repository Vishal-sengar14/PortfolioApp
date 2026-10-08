using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class CertificatesController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public CertificatesController(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _uow.Certificates.GetAllAsync()).OrderBy(c => c.DisplayOrder).ToList();
            return View(list);
        }

        public IActionResult Create() => View(new Certificate());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Certificate model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid) return View(model);

            var path = await FileUploadHelper.SaveFileAsync(imageFile, _env, "images/certificates");
            if (path != null) model.ImagePath = path;

            await _uow.Certificates.AddAsync(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Certificate added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _uow.Certificates.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Certificate model, IFormFile? imageFile)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            var path = await FileUploadHelper.SaveFileAsync(imageFile, _env, "images/certificates");
            if (path != null) model.ImagePath = path;

            _uow.Certificates.Update(model);
            await _uow.SaveChangesAsync();
            TempData["Success"] = "Certificate updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _uow.Certificates.GetByIdAsync(id);
            if (item != null)
            {
                _uow.Certificates.Remove(item);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Certificate deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
