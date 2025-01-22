using System.Diagnostics;
using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using BlogCore_ASPNetMVC_Net8.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore_ASPNetMVC_Net8.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var homeVM = new HomeVM
            {
                Sliders = _workContainer.SliderRepository.GetAll(),
                Articles = _workContainer.ArticleRepository.GetAll()
            };

            ViewBag.IsHome = true;

            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
