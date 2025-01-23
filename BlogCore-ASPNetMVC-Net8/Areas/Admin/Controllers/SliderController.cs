using BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository;
using BlogCore_ASPNetMVC_Net8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore_ASPNetMVC_Net8.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SliderController(IWorkContainer workContainer, IWebHostEnvironment hostingEnvironment)
        {
            _workContainer = workContainer;
            _hostingEnvironment = hostingEnvironment;
        }

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
        public IActionResult Create(Slider slider)
        {
            if (ModelState.ErrorCount < 2)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    // New Article
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\sliders");
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    slider.URLImage = @"\images\sliders\" + fileName + extension;

                    _workContainer.SliderRepository.Add(slider);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Image", "Image is required");
                }
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Slider slider = _workContainer.SliderRepository.Get(id.GetValueOrDefault());
                return View(slider);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.ErrorCount < 2)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var sliderFromDb = _workContainer.SliderRepository.Get(slider.Id);

                if (files.Count() > 0)
                {
                    // new image for article
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\sliders");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(mainPath, sliderFromDb.URLImage.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    // upload new image
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    slider.URLImage = @"\images\sliders\" + fileName + extension;
                }
                else
                {
                    slider.URLImage = sliderFromDb.URLImage;
                }

                _workContainer.SliderRepository.Update(slider);
                _workContainer.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.SliderRepository.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workContainer.SliderRepository.Get(id);
            string mainPath = _hostingEnvironment.WebRootPath;
            var imagePath = Path.Combine(mainPath, objFromDb.URLImage.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error deleting slider" });
            }

            _workContainer.SliderRepository.Remove(objFromDb);
            _workContainer.Save();
            return Json(new { success = true, message = "Slider deleted successfully" });
        }

        #endregion
    }
}
