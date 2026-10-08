using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.ViewModels;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;

        public DashboardController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                TotalProjects = await _uow.Projects.CountAsync(),
                TotalSkills = await _uow.Skills.CountAsync(),
                TotalExperiences = await _uow.Experiences.CountAsync(),
                TotalEducations = await _uow.Educations.CountAsync(),
                TotalMessages = await _uow.Messages.CountAsync(),
                UnreadMessages = await _uow.Messages.CountAsync(m => !m.IsRead),
                TotalCertificates = await _uow.Certificates.CountAsync(),
                TotalTestimonials = await _uow.Testimonials.CountAsync()
            };

            vm.ChartData = new()
            {
                ("Projects", vm.TotalProjects),
                ("Skills", vm.TotalSkills),
                ("Experience", vm.TotalExperiences),
                ("Education", vm.TotalEducations),
                ("Messages", vm.TotalMessages),
                ("Certificates", vm.TotalCertificates),
                ("Testimonials", vm.TotalTestimonials)
            };

            return View(vm);
        }
    }
}
