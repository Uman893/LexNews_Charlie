using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;

namespace LexNews_Charlie.Models.ViewModels
{
    public class SubscriptionTypeVM
    {
        [Display(Name = "Type of subscriptions")]
        public List<SelectListItem> SubscriptionTypesList { get; set; }
        public string SelectedSubscriptionType { get; set; }

        [Required(ErrorMessage = "Please enter a Card Number")]
     
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter a CardHolder's Name")]
        public string CardHoldersName { get; set; }

        [Required(ErrorMessage = "Please enter a Expiration Date")]
        [RegularExpression(@"(0[1-9]|1[0-2])\/(([0-9]{4}|[0-9]{2})$)", ErrorMessage = "Please enter a valid Expiration Date")] //RegexExpression, good to use for validation
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Please enter your CVV numbers")]
        [RegularExpression(@"[0-9]{3,4}$", ErrorMessage ="Please enter a valid Cvv")]
        public int CVV { get; set; }

        public SubscriptionTypeVM()
        {
            SubscriptionTypesList = new List<SelectListItem>();

        }
    }

}
