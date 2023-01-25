using LexNews_Charlie.Data;
using LexNews_Charlie.Models.ViewModels;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace LexNews_Charlie.ViewComponents
{
    public class LatestNewsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IStorageService _storageService;
        public LatestNewsViewComponent(ApplicationDbContext db, IStorageService storageService)
        {
            _db = db;
            _storageService = storageService;
        }
        public IViewComponentResult Invoke()
        {
           var LatestNews = _db.Articles.OrderByDescending(m => m.DateStamp).Where(a => a.Archived != true).Take(5).ToList();
            foreach (var item in LatestNews)
            {
                string containerName = "news-images-sm";
                item.ImageLink = _storageService.GetBlob(item.FileName, containerName);
            }

            return View("Index",LatestNews);
        }
    }
}
