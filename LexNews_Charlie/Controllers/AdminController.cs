using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LexNews_Charlie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace LexNews_Charlie.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IStorageService _storageService;
        private readonly IMapper _iMapper;

        private readonly ApplicationDbContext _db;
        private readonly IRoleService _roleService;
        public AdminController(IRoleService roleService, ApplicationDbContext db, 
            ILogger<AdminController> logger, RoleManager<IdentityRole> roleManager, IUserService userService, IConfiguration configuration
            , IArticleService articleService, ICategoryService categorySerivce, IStorageService storageService, IMapper iMapper)
        {        
            _logger = logger;
            _roleManager = roleManager;
            _userService = userService;
            _db = db;
            _roleService = roleService;
            _configuration = configuration;
            _articleService = articleService;
            _categoryService = categorySerivce;
            _storageService = storageService;
            _iMapper = iMapper;
        }
        public IActionResult Administration()
        {
            return View();
        }
        [Authorize(Roles = "Editor")]
        public IActionResult GetArticlesList()
        {
            List<Article> articlesList = _articleService.GetArticlesList();
            return View(articlesList);
        }

        [Authorize(Roles = "Editor")]
        [HttpGet]
        public IActionResult EditorsChoice()
        {
            List<Article> articlesList = _articleService.GetArticlesList();
            return View(articlesList); 
        }
        [HttpPost]
        public IActionResult EditorsChoice(int id)
        {
            var article = _articleService.GetOneArticlebyId(id);
            if (article == null)
            {
                return Json(new { editorsChoice = false });
            }
            article.EditorsChoice = !article.EditorsChoice;

            _db.Update(article);
            _db.SaveChanges();
            return Json(new { editorsChoice = article.EditorsChoice });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoriesData = _categoryService.GetCategoryList();
            var model = new DisplayArticleVM();
            model.CategoriesList = new List<SelectListItem>();
            foreach (var item in categoriesData)
            {
                model.CategoriesList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Create (DisplayArticleVM newArticle)
        {
            string FolderPath = "wwwroot/images/articles" + "/" + newArticle.CategoryId + "/";
            string path = Path.Combine(Directory.GetCurrentDirectory(), FolderPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileNameWithPath = Path.Combine(path, newArticle.FileName);
            using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                newArticle.File.CopyTo(stream);
            }

            string pathfile = newArticle.CategoryId + "/" + newArticle.File.FileName;

            Uri blobUri = _storageService.UploadBlob(pathfile);         

            _articleService.SaveArticle(newArticle, blobUri);    

            return RedirectToAction("GetArticlesList");
        }
        public IActionResult Details(int id)
        {
            var article = _articleService.DetailsArticle(id);
            return View(article);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Article article = _articleService.FetchArticle(Id);
            if (article == null)
            {
                return NotFound();
            }
            var categoriesData = _categoryService.GetCategoryList();
            var model = new DisplayArticleVM();            
            model.CategoriesList = new List<SelectListItem>();
            foreach (var item in categoriesData)
            {
                model.CategoriesList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            DisplayArticleVM newEditedArticle = _iMapper.Map<DisplayArticleVM>(article);
            newEditedArticle.CategoriesList = model.CategoriesList;

            return View(newEditedArticle);
        }
        [HttpPost]
        public IActionResult Edit(DisplayArticleVM newEditedArticle, int Id)
        {

            var article = _articleService.GetOneArticlebyId(Id);
            string image = article.ImageLink.ToString();
            if(image != null)
            {
                _storageService.DeleteBlob(article);
            }

            string FolderPath = "wwwroot/images/articles" + "/" + newEditedArticle.CategoryId + "/";
            string path = Path.Combine(Directory.GetCurrentDirectory(), FolderPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileNameWithPath = Path.Combine(path, newEditedArticle.FileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                newEditedArticle.File.CopyTo(stream);
            }

            string pathfile = newEditedArticle.CategoryId + "/" + newEditedArticle.File.FileName;

            Uri blobUri = _storageService.UploadBlob(pathfile);

            _articleService.EditAndSaveArticle(newEditedArticle, blobUri);

            return RedirectToAction("GetArticlesList");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Article article = _articleService.DeleteArticle(id);
            return View(article);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var article = _articleService.GetOneArticlebyId(id);
            string image = article.ImageLink.ToString();
            if (article != null)
            {
                _storageService.DeleteBlob(article);
                _articleService.DeleteConfirmedArticle(id);
            }          
            _db.SaveChanges();            
            
            return RedirectToAction(nameof(GetArticlesList));
        }     
     
        [Authorize(Roles = "Admin")]
        public IActionResult SubscriptionStatistics()
        {
            int[] data = new int[30];
            for (int i = 0; i > -30; i--)
            {
                data[-i] = _db.Subscription.Where(x => x.Created.Date == DateTime.Now.AddDays(i).Date).Count();

            }
            ViewBag.subscriptions = data;

            var premium = _db.Subscription.Where(x => x.SubscriptionType.Id == 3 && x.Active).ToList().Count();
            var basic = _db.Subscription.Where(x => x.SubscriptionType.Id == 2 && x.Active).ToList().Count();
            var free = _db.Subscription.Where(x => x.SubscriptionType.Id == 1 && x.Active).ToList().Count();

            ViewBag.piesubscriptions = new int[] { premium, basic, free };
            return View();
        }
    }
}
