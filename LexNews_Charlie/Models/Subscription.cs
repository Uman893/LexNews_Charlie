using System.ComponentModel.DataAnnotations;

namespace LexNews_Charlie.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Creation Time")]
        public DateTime Created { get; set; }
        [Required]
        [Display(Name = "Expiration Time")]
        public DateTime Expires { get; set; }
        public virtual User User { get; set; }
        public bool Active { get; set; }
        public Subscription()
        {

        }
        public Subscription(int id, SubscriptionType subscriptionType, decimal price, DateTime created, DateTime expires, User user, bool active)
        {
            Id = id;
            SubscriptionType = subscriptionType;
            Price = price;
            Created = created;
            Expires = expires;
            User = user;
            Active = active;
        }
    }
}
