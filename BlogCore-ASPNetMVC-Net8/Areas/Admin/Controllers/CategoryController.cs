using BlogCore_ASPNetMVC_Net8.Data;
using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogCore_ASPNetMVC_Net8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly ApplicationDbContext _context;

        public CategoryController(IWorkContainer workContainer, ApplicationDbContext context)
        {
            _workContainer = workContainer;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _workContainer.CategoryRepository.Add(category);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = new Category();
            category = _workContainer.CategoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _workContainer.CategoryRepository.Update(category);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            //return Json(new { data = _workContainer.CategoryRepository.GetAll() });
            // Calling a stored procedure
            return Json(new {data = _context.Category.FromSqlRaw<Category>("spGetCategories").ToList() } );
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workContainer.CategoryRepository.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error deleting category" });
            }

            _workContainer.CategoryRepository.Remove(objFromDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Category deleted successfully" });
        }


        #endregion
    }
}
