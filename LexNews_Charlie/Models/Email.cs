using System.ComponentModel.DataAnnotations;

namespace LexNews_Charlie.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Subscriber Email")]
        public string SubscriberEmail { get; set; }
        [Required]
        [Display(Name ="Subscriber Name")]
        public string SubscriberName { get; set; }
        [Required]
        [Display(Name ="Subscription Type")]
        public string SubscriptionType { get; set; }
    }
}
