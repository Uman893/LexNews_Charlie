using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.ViewComponents
{
    public class TitlesOnlyViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public TitlesOnlyViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {

            List<Article> dbArticles = _db.Articles.OrderByDescending(a => a.Views).Where(a => a.Archived != true).Take(12).ToList();
            return View("Index", dbArticles);
        }
    }
}
