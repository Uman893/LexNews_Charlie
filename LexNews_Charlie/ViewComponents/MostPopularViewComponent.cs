using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LexNews_Charlie.ViewComponents
{
    public class MostPopularViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IStorageService _storageService;
        public MostPopularViewComponent(ApplicationDbContext db, IStorageService storageService)
        {
            _db = db;
            _storageService = storageService;
        }
        public IViewComponentResult Invoke()
        {
            var MostPopularNews = _db.Articles.Where(v => v.Views > 1 && v.Archived != true).Take(8).ToList();
            foreach (var item in MostPopularNews)
            {
                string containerName = "news-images-xs";
                item.ImageLink = _storageService.GetBlob(item.FileName, containerName);
            }
            return View("Index", MostPopularNews);
        }
    }
}
