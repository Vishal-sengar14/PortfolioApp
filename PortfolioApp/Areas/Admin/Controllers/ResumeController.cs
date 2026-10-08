using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class ResumeController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _env;

        public ResumeController(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var resume = (await _uow.Resumes.FindAsync(r => r.IsActive)).FirstOrDefault();
            return View(resume);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile resumeFile)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                TempData["Error"] = "Please choose a PDF file to upload.";
                return RedirectToAction(nameof(Index));
            }

            var path = await FileUploadHelper.SaveFileAsync(resumeFile, _env, "uploads/resume");

            var existing = (await _uow.Resumes.GetAllAsync()).FirstOrDefault();
            if (existing != null)
            {
                existing.FileName = resumeFile.FileName;
                existing.FilePath = path!;
                existing.UploadedOn = DateTime.UtcNow;
                existing.IsActive = true;
                _uow.Resumes.Update(existing);
            }
            else
            {
                await _uow.Resumes.AddAsync(new Resume
                {
                    FileName = resumeFile.FileName,
                    FilePath = path!,
                    IsActive = true
                });
            }

            await _uow.SaveChangesAsync();
            TempData["Success"] = "Resume uploaded successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
