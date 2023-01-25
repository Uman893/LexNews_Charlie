using LexNews_Charlie.Models;
using LexNews_Charlie.Services;
using Microsoft.AspNetCore.Mvc;

namespace LexNews_Charlie.Controllers
{
    public class SendEmailController : Controller
    {
        private readonly ILogger<SendEmailController> _logger;
        private readonly IEmailService _emailService;

        public SendEmailController(ILogger<SendEmailController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult CreateSubscription()
        {
            Email newEmail = new()
            {
                SubscriberEmail = "Adadyakob@hotmail.com",
                SubscriberName = "Adad",
                SubscriptionType = "Basic"
            };
            TempData["ShowMessage"] = SendConfirmation(newEmail);
            return RedirectToAction("Index", "SendEmail");
        }
        public string SendConfirmation(Email newEmail)
        {
            return _emailService.SendSubscriptionEmail(newEmail).Result;
        }
    }
}
