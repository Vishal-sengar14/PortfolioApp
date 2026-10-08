using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class ProfileController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public ProfileController(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var profile = (await _uow.Profiles.GetAllAsync()).FirstOrDefault() ?? new Profile();
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Profile model, IFormFile? photoFile)
        {
            if (!ModelState.IsValid) return View(model);

            var path = await FileUploadHelper.SaveFileAsync(photoFile, _env, "images/profile");
            if (path != null) model.ProfileImagePath = path;

            var existing = model.Id != 0 ? await _uow.Profiles.GetByIdAsync(model.Id) : null;
            if (existing == null)
            {
                await _uow.Profiles.AddAsync(model);
            }
            else
            {
                if (path == null) model.ProfileImagePath = existing.ProfileImagePath;
                _uow.Profiles.Update(model);
            }

            await _uow.SaveChangesAsync();
            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
