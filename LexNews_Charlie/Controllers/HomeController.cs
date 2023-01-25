using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using LexNews_Charlie.Data;
using LexNews_Charlie.Models.ViewModels;
using LexNews_Charlie.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Host;
using LexNews_Charlie.Services;
using Microsoft.Extensions.Logging;
using LexNews_Charlie.Models.Entities;

namespace LexNews_Charlie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ICurrencyService _currencyService;
        private readonly IConfiguration _configuration;
        private readonly IArticleService _articleService;
        private readonly IStorageService _storageService;


        const string sessionkey = "Sessionkey";

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, ICurrencyService currencyService,
            IConfiguration configuration, IArticleService articleService, IStorageService storageService)
        {
            _logger = logger;
            _context = context;
            _currencyService = currencyService;
            _configuration = configuration;
            _articleService = articleService;
            _storageService = storageService;
        }

        public IActionResult Index()
        {          
            return View(_articleService.GetArticlesList());
        }

        public IActionResult Privacy()
        {
            var tempstring = TempData["ShowMessage"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NewsFilter(string searchInput)
        {
            var AllNews = _context.Articles.ToList();
            if (!string.IsNullOrEmpty(searchInput))
            {
                var filteredResultNew = AllNews.Where(n => n.Title.Contains(searchInput) || n.Content.Contains(searchInput)).ToList();
                return View("OneNewsCard", filteredResultNew);
            }
            return View("OneNewsCard", AllNews);

        }

        [HttpPost]
        public IActionResult GetCurrency(string from, string to, decimal q)
        {
            var exchangerate = _currencyService.GetCurrency(from, to, q).Result;
            return Json(exchangerate);
        }
        public void StorePriceData()
        {
            _storageService.AddSpotPriceToTable();
        }
        public IActionResult GetAreaPrices()
        {
            var se1 = _storageService.GetEntities("SE1");
            var se2 = _storageService.GetEntities("SE2");
            var se3 = _storageService.GetEntities("SE3");
            var se4 = _storageService.GetEntities("SE4");
            var values= se1.Concat(se2).Concat(se3).Concat(se4);
            var orderedByDate = values.GroupBy(d => d.DateAndTime)
                .Select(o => new HistoricalDataVM()
                {
                    Date = o.Key,
                    DateAreaPrices = o.ToList()
                }).ToList();
            return View(orderedByDate);
        }
                
        public IActionResult VisualizeElectricPrices()
        {
            return View(PriceList());
        }
        public List<HistoricalDataVM> PriceList()
        {
            var se1 = _storageService.GetEntities("SE1");
            var se2 = _storageService.GetEntities("SE2");
            var se3 = _storageService.GetEntities("SE3");
            var se4 = _storageService.GetEntities("SE4");
            var values = se1.Concat(se2).Concat(se3).Concat(se4);
            var orderedByDate = values.GroupBy(d => d.DateAndTime)
                .Select(o => new HistoricalDataVM()
                {
                    Date = o.Key,
                    DateAreaPrices = o.ToList()
                }).ToList();

            return orderedByDate;
        }
        public IActionResult ShowElectricPrices()
        {
            return View(LowPriceList());
        }
        public List<HistoricalDataVM> LowPriceList()
        {
            var se1 = _storageService.GetEntities("SE1");
            var se2 = _storageService.GetEntities("SE2");
            var se3 = _storageService.GetEntities("SE3");
            var se4 = _storageService.GetEntities("SE4");
            var values = se1.Concat(se2).Concat(se3).Concat(se4);
            var orderedByDate = values.GroupBy(d => d.DateAndTime)
                .Select(o => new HistoricalDataVM()
                {
                    Date = o.Key,
                    DateAreaPrices = o.ToList()
                }).ToList();

            return orderedByDate;
        }
        public IActionResult ShowCharts()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
    }
}