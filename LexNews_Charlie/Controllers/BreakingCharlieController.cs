using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using LexNews_Charlie.Models.ViewModels;
using LexNews_Charlie.Data;
using AutoMapper;
using LexNews_Charlie.Services;
using NuGet.Versioning;
using System.Text.Json;
using Newtonsoft.Json;

namespace LexNews_Charlie.Controllers
{
    public class BreakingCharlieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;
        private readonly IWeaterForcastService _weaterForcastService;
        private readonly IStorageService _storageService;
        private readonly ISoccerScoresService _soccerScoresService;

        public BreakingCharlieController(ApplicationDbContext context, IMapper mapper, IArticleService articleService, IWeaterForcastService weaterForcastService,
        IStorageService storageService, ISoccerScoresService soccerScoresService)

        {
            _context = context;
            _mapper = mapper;
            _articleService = articleService;
            _weaterForcastService = weaterForcastService;
            _storageService = storageService;
            _soccerScoresService = soccerScoresService;

        }    
 
        public IActionResult DisplayCategory(int id)
        {
            var categoryArticles = _articleService.GetArticles(id);
            foreach (var item in categoryArticles)
            {
                string containerName = "news-images-sm";
                item.ImageLink = _storageService.GetBlob(item.FileName, containerName);
            }
            return View(categoryArticles);
        }

        public IActionResult DisplCompleteArticle(int id)
        {
            var CompleteArticle = _context.Articles.FirstOrDefault(a => a.Id == id);
            string containerName = "news-images-sm";
            if (CompleteArticle != null)
            {
                ++CompleteArticle.Views;
                CompleteArticle.ImageLink = _storageService.GetBlob(CompleteArticle.FileName, containerName);
            }

            _context.SaveChanges();
            return View(CompleteArticle);
        }

        public IActionResult Like(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article != null)
                ++article.Likes;
            _context.SaveChanges();
            return Json(new { count = article.Likes });
        }

        public IActionResult Dislike(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article != null)
                ++article.DisLikes;
            _context.SaveChanges();
            return Json(new { count = article.DisLikes });
        }


        [HttpPost]
        public async Task<IActionResult> GetWeather(string City)
        {
            var weatherApi = await _weaterForcastService.GetWeather(City);
            DisplayWeatherVM result = new DisplayWeatherVM();
            result.Country = weatherApi.sys.country;
            result.City = weatherApi.name;
            result.Description = weatherApi.weather[0].description;
            result.Humidity = Convert.ToString(weatherApi.main.humidity);
            result.TempFeelsLike = Convert.ToString(weatherApi.main.feels_like);
            result.Temp = Convert.ToString(weatherApi.main.temp);
            result.WeatherIcon = weatherApi.weather[0].icon;
            return Json(result);

        }

        public IActionResult Weather()
        {
            return View();
        }
               
        public async Task<IActionResult> GetScores(string sport)
        {
            var scoresAPI = await _soccerScoresService.GetScores(sport);            
            return View(scoresAPI);
        }

    }
}
