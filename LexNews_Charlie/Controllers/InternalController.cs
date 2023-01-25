using LexNews_Charlie.Data;
using LexNews_Charlie.Models;
using LexNews_Charlie.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LexNews_Charlie.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Storage.Blobs.Models;

namespace LexNews_Charlie.Controllers
{
    public class InternalController : Controller
    {
        const string UserSessionKey = "SessionUserKey";
        private readonly ApplicationDbContext _db;
        private readonly ILogger<InternalController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ISubscriptionTypeService _IsubscriptionTypeService;
        public InternalController(ApplicationDbContext db, ILogger<InternalController> logger, UserManager<User> userManager, ISubscriptionTypeService subscriptiontypeservice)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
            _IsubscriptionTypeService = subscriptiontypeservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult SubscribeNow(int id)
        {
            var subscriptiontypedata = _IsubscriptionTypeService.GetSubscriptionTypeList();
            var Model = new SubscriptionTypeVM();
            Model.SubscriptionTypesList = new List<SelectListItem>();
            foreach (var item in subscriptiontypedata)
            {
                Model.SubscriptionTypesList.Add(new SelectListItem { Text = item.TypeName + " " + item.Price + " " + " Sek", Value = item.Id.ToString() });
            }
            return View(Model);
        }
        [HttpPost]
        public IActionResult SubscribeNow(SubscriptionTypeVM model)
        {
            var selected = model.SelectedSubscriptionType;
            return RedirectToAction("SubscribeNow");
        }
        public IActionResult CompleteSubscription(SubscriptionTypeVM model)
        {
            var user = _userManager.GetUserAsync(User).Result.Id;
            int selected = int.Parse(model.SelectedSubscriptionType);
            var subscriptiontypedata = _IsubscriptionTypeService.GetSubscriptionTypeList();
            var sum = subscriptiontypedata.Where(s => s.Id == selected).FirstOrDefault().Price;        
            
           
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    Subscription ny = new Subscription
                    {
                        User = _db.Users.Find(user),
                        SubscriptionType = _db.SubscriptionType.Find(selected),
                        Created = DateTime.Now,
                        Expires = DateTime.Now.AddDays(30),
                        Price = sum,   
                        Active = true                        
                    };
                    _db.Subscription.Add(ny);
                    _db.SaveChanges();                    
                }
                else 
                {
                    throw new Exception("Your subscription is not completed");
                }
            }            
            return RedirectToAction("MyPage");
        }
        public IActionResult MyPage()
        {
            return View();
        }
        public IActionResult MySubscriptions()
        {
            var user = _userManager.GetUserAsync(User).Result;          
           
            if(user == null)
            {
                return RedirectToAction("MyPage");
            }
            else
            {                      
                var MySubscriptions = _db.Subscription.Where(u => u.User == user)
                    .Select(_db => new SubscriptionVM()
                    {
                        Active = _db.Active,
                        Price = _db.Price,
                        CreatedAt = _db.Created,
                        ExpiresAt = _db.Expires

                    }).ToList();
                return View(MySubscriptions);
            }             
        }
        public IActionResult NewsLetter()
        {            
            return View();
        }
    }
}
