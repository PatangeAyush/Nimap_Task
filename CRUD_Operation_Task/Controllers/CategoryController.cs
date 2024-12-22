using CRUD_Operation_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation_Task.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Logic logic;

        public CategoryController(Logic logic)
        {
            this.logic=logic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCategories() 
        {
            var data = logic.GetCategories();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddCategory()
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(string CategoryName)
        {
            try
            {
                logic.AddCategory(CategoryName);
                return RedirectToAction("GetCategories");
            }
            catch
            {
                return View();
            }          
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        { 
            var data = logic.GetCategories().Find(model => model.CategoryID == id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(CategoryDTO update)
        {
            try
            {
                logic.UpdateCategory(update);
                return RedirectToAction("GetCategories");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var data = logic.GetCategories().Find(model => model.CategoryID == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id, IFormCollection collection)
        {
            try
            {
                logic.DeleteCategory(id);
                return RedirectToAction("GetCategories");
            }
            catch
            {
                return View();
            }
        }
    }
}
