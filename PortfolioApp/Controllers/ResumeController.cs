using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;

namespace PortfolioApp.Controllers
{
    public class ResumeController : Controller
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
        public async Task<IActionResult> Preview()
        {
            var resume = (await _uow.Resumes.FindAsync(r => r.IsActive)).FirstOrDefault();
            if (resume == null) return NotFound();

            var fullPath = Path.Combine(_env.WebRootPath, resume.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(fullPath)) return NotFound();

            var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(bytes, "application/pdf");   // fileDownloadName NAHI diya — isliye inline dikhega
        }

        public async Task<IActionResult> Download()
        {
            var resume = (await _uow.Resumes.FindAsync(r => r.IsActive)).FirstOrDefault();
            if (resume == null) return NotFound();

            var fullPath = Path.Combine(_env.WebRootPath, resume.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(fullPath)) return NotFound();

            var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(bytes, "application/pdf", resume.FileName);
        }
    }
}
