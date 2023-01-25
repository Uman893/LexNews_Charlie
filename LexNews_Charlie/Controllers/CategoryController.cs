using LexNews_Charlie.Models;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LexNews_Charlie.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService= categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult GetCategoryList()
        {
            List<Category> categoriesList = _categoryService.GetCategoryList();
            return View(categoriesList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _categoryService.CreateCategory(category);          
            return RedirectToAction(nameof(GetCategoryList));
        }
        public IActionResult Details(int id)
        {
            var category = _categoryService.DetailsCategory(id);
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _categoryService.FetchCategory(id);            
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryService.EditCategory(category);           
            return RedirectToAction(nameof(GetCategoryList));
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Category category = _categoryService.DeleteCategory(id);
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _categoryService.DeleteConfirmedCategory(id);
            return RedirectToAction(nameof(GetCategoryList));
        }
    }
}
