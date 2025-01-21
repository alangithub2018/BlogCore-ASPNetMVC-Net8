using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore_ASPNetMVC_Net8.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ArticleController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
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
