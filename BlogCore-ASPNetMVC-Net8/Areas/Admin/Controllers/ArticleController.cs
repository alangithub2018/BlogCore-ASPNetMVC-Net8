using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore_ASPNetMVC_Net8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticleController(
            IWorkContainer workContainer,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _workContainer = workContainer;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleVM articleVM = new ArticleVM() 
            {
                Article = new BlogCore_ASPNetMVC_Net8.Models.Article() { },
                CategoryList = _workContainer.CategoryRepository.GetCategoryList()
            };

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleVM)
        {
            if (ModelState.ErrorCount < 4)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (articleVM.Article.Id == 0 && files.Count() > 0)
                {
                    // New Article
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\articles");
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    articleVM.Article.URLImage = @"\images\articles\" + fileName + extension;
                    articleVM.Article.CreatedDate = DateTime.Now.ToString();

                    _workContainer.ArticleRepository.Add(articleVM.Article);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Image", "Image is required");
                }
            }

            articleVM.CategoryList = _workContainer.CategoryRepository.GetCategoryList();
            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleVM)
        {
            if (ModelState.ErrorCount < 4)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var articleFromDb = _workContainer.ArticleRepository.Get(articleVM.Article.Id);

                if (files.Count() > 0)
                {
                    // new image for article
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\articles");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(mainPath, articleFromDb.URLImage.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    // upload new image
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    articleVM.Article.URLImage = @"\images\articles\" + fileName + extension;
                    articleVM.Article.CreatedDate = DateTime.Now.ToString();
                }
                else
                {
                    articleVM.Article.URLImage = articleFromDb.URLImage;
                }

                _workContainer.ArticleRepository.Update(articleVM.Article);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            articleVM.CategoryList = _workContainer.CategoryRepository.GetCategoryList();
            return View(articleVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = _workContainer.ArticleRepository.Get(id.GetValueOrDefault()),
                CategoryList = _workContainer.CategoryRepository.GetCategoryList()
            };
            if (articleVM.Article == null)
            {
                return NotFound();
            }

            return View(articleVM);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.ArticleRepository.GetAll(includeProperties: "Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workContainer.ArticleRepository.Get(id);
            string mainPath = _hostingEnvironment.WebRootPath;
            var imagePath = Path.Combine(mainPath, objFromDb.URLImage.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error deleting article" });
            }

            _workContainer.ArticleRepository.Remove(objFromDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Article deleted successfully" });
        }

        #endregion
    }
}
