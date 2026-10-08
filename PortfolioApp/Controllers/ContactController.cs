using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;
using PortfolioApp.Models;
using PortfolioApp.Services.Interfaces;
using PortfolioApp.ViewModels;

namespace PortfolioApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public ContactController(IUnitOfWork uow, IEmailService emailService, IConfiguration config)
        {
            _uow = uow;
            _emailService = emailService;
            _config = config;
        }

        public IActionResult Index()
        {
            var vm = new ContactViewModel();
            GenerateCaptcha(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            var expected = HttpContext.Session.GetInt32("CaptchaAnswer");
            if (expected == null || model.CaptchaInput != expected)
            {
                ModelState.AddModelError(nameof(model.CaptchaInput), "Incorrect captcha answer, please try again.");
            }

            if (!ModelState.IsValid)
            {
                GenerateCaptcha(model);
                return View(model);
            }

            var message = new Message
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Subject = model.Subject,
                Body = model.Message
            };

            await _uow.Messages.AddAsync(message);
            await _uow.SaveChangesAsync();

            var adminEmail = _config["SmtpSettings:AdminNotificationEmail"];
            if (!string.IsNullOrWhiteSpace(adminEmail))
            {
                var html = $"<h3>New Contact Message</h3>" +
                           $"<p><b>Name:</b> {model.Name}</p>" +
                           $"<p><b>Email:</b> {model.Email}</p>" +
                           $"<p><b>Phone:</b> {model.Phone}</p>" +
                           $"<p><b>Subject:</b> {model.Subject}</p>" +
                           $"<p><b>Message:</b><br/>{model.Message}</p>";
                await _emailService.SendEmailAsync(adminEmail, $"New Portfolio Message: {model.Subject}", html);
            }

            TempData["ContactSuccess"] = "Thank you! Your message has been sent successfully.";
            return RedirectToAction(nameof(Index));
        }

        private void GenerateCaptcha(ContactViewModel model)
        {
            var rnd = new Random();
            model.CaptchaFirstNumber = rnd.Next(1, 10);
            model.CaptchaSecondNumber = rnd.Next(1, 10);
            HttpContext.Session.SetInt32("CaptchaAnswer", model.CaptchaFirstNumber + model.CaptchaSecondNumber);
        }
    }
}
