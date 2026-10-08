using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Data.Repositories.Interfaces;

namespace PortfolioApp.Areas.Admin.Controllers
{
    public class MessagesController : AdminBaseController
    {
        private readonly IUnitOfWork _uow;

        public MessagesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var messages = (await _uow.Messages.GetAllAsync()).OrderByDescending(m => m.SentOn).ToList();
            return View(messages);
        }

        public async Task<IActionResult> Details(int id)
        {
            var message = await _uow.Messages.GetByIdAsync(id);
            if (message == null) return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                _uow.Messages.Update(message);
                await _uow.SaveChangesAsync();
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _uow.Messages.GetByIdAsync(id);
            if (message != null)
            {
                _uow.Messages.Remove(message);
                await _uow.SaveChangesAsync();
                TempData["Success"] = "Message deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
