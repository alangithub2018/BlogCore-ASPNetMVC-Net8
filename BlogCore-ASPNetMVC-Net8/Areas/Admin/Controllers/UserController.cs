using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore_ASPNetMVC_Net8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public UserController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Get all users except the logged in user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var currentUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_workContainer.UserRepository.GetAll(u => u.Id != currentUser.Value));
        }

        [HttpGet]
        public IActionResult Deactivate(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _workContainer.UserRepository.DeactivateUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Activate(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _workContainer.UserRepository.ActivateUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
