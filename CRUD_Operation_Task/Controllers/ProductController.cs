using CRUD_Operation_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CRUD_Operation_Task.Controllers
{
    public class ProductController : Controller
    {
        private readonly Logic logic;

        public ProductController(Logic logic)
        {
            this.logic=logic;
        }

        public ActionResult Index(int page = 1)
        {
            try
            {
                int pageSize = 10;


                var allData = logic.GetCombinedProductCategoryData();


                var pagedData = allData.Skip((page - 1) * pageSize).Take(pageSize).ToList();


                int totalRecords = allData.Count();
                int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);


                ViewData["TotalPages"] = totalPages;
                ViewData["CurrentPage"] = page;

                return View(pagedData);
            }
            catch
            {             
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            var data = logic.GetProducts();
            return View(data);         
          
        }

        // GET: ProductController/AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            var Dropdown = logic.GetProductCategories();
            ViewBag.Dropdown = new SelectList(Dropdown,"CategoryID","CategoryName");
            return View();
        }

        // POST: ProductController/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductDTO add)
        {
            try
            {
                logic.AddProduct(add);
                return RedirectToAction("GetProducts");
            }   
            catch
            {                        
                return View();
            }
            
        }

        // GET: ProductController/Update/5
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            var data = logic.GetProducts().Find(model => model.ProductID == id);

            var Dropdown = logic.GetProductCategories();
            ViewBag.Dropdown = new SelectList(Dropdown, "CategoryID", "CategoryName");

            return View(data);
        }

        // POST: ProductController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(int id,ProductDTO update)
        {
            try
            {
                logic.UpdateProduct(update);
                return RedirectToAction("GetProducts");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        // GET: ProductController/Delete/5
        public ActionResult DeleteProduct(int id)
        {
            var data = logic.GetProducts().Find(model => model.ProductID == id);
            return View(data);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, IFormCollection collection)
        {
            try
            {
                logic.DeleteProduct(id);
                return RedirectToAction("GetProducts");
            }
            catch
            {
                return View();
            }
        }
    }
}
