using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.ViewComponents
{
    public class EditorsChoiceViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IStorageService _storageService;

        public EditorsChoiceViewComponent(ApplicationDbContext db, IStorageService storageService)
        {
            _db = db;
            _storageService = storageService;  
        }

        public IViewComponentResult Invoke()
        {
            var articles = _db.Articles.Where(a => a.EditorsChoice == true && a.Archived != true).Take(8).ToList();
            foreach (var item in articles)
            {
                string containerName = "news-images-xs";
                item.ImageLink = _storageService.GetBlob(item.FileName, containerName);
            }
            return View(articles);

        }
    }
}
