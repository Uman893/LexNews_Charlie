using LexNews_Charlie.Models;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.Controllers
{
    public class SubscriptionTypeController : Controller
    {
  
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        public SubscriptionTypeController(ISubscriptionTypeService subscriptionTypeService)
        {         
            _subscriptionTypeService = subscriptionTypeService;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult GetSubscriptionTypeList()
        {
            List<SubscriptionType> subscriptionTypesList = _subscriptionTypeService.GetSubscriptionTypeList();
            return View(subscriptionTypesList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SubscriptionType subscriptionType)
        {
            _subscriptionTypeService.CreateSubscriptionType(subscriptionType);
            return RedirectToAction(nameof(GetSubscriptionTypeList));
        }
        public IActionResult Details(int id)
        {
            var subscriptionType = _subscriptionTypeService.DetailsSubscriptionType(id);
            return View(subscriptionType);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SubscriptionType subscriptionType = _subscriptionTypeService.FetchSubscriptionType(id);            
            return View(subscriptionType);
        }   
        [HttpPost]
        public IActionResult Edit(SubscriptionType subscriptionType)
        {
            _subscriptionTypeService.EditSubscriptionType(subscriptionType);         
            return RedirectToAction(nameof(GetSubscriptionTypeList));
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            SubscriptionType subscriptionType = _subscriptionTypeService.DeleteSubscriptionType(id);         
            return View(subscriptionType);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var subscriptionType = _subscriptionTypeService.DeleteConfirmedSubscriptionType(id);
            return RedirectToAction(nameof(GetSubscriptionTypeList));
        }
    }
}
