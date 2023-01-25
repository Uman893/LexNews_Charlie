using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LexNews_Charlie.ViewComponents
{

    public class MenuCategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public MenuCategoriesViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {

            List<Category> dbCategories = _db.Categories.ToList();
            return View("Index", dbCategories);            
        }
    }
}
