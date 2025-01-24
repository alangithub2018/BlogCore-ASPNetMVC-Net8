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

        // First version no pagination
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var homeVM = new HomeVM
        //    {
        //        Sliders = _workContainer.SliderRepository.GetAll(),
        //        Articles = _workContainer.ArticleRepository.GetAll()
        //    };

        //    ViewBag.IsHome = true;

        //    return View(homeVM);
        //}

        // Second version with pagination
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var articles = _workContainer.ArticleRepository.AsQueryable();
            var paginatedEntries = articles.Skip((page - 1) * pageSize).Take(pageSize);
            var homeVM = new HomeVM
            {
                Sliders = _workContainer.SliderRepository.GetAll(),
                Articles = paginatedEntries.ToList(),
                PageIndex = page,
                TotalPages = (int)System.Math.Ceiling(articles.Count() / (double)pageSize)
            };

            ViewBag.IsHome = true;

            return View(homeVM);
        }

        // To search for articles

        [HttpGet]
        public IActionResult SearchResult(string searchString, int page = 1, int pageSize = 3)
        {
            var articles = _workContainer.ArticleRepository.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(a => a.Name.Contains(searchString) || a.Description.Contains(searchString));
            }

            // Pagination
            var paginated = articles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new PaginatedList<Article>(paginated.ToList(), articles.Count(), page, pageSize, searchString);

            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var article = _workContainer.ArticleRepository.GetFirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
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
