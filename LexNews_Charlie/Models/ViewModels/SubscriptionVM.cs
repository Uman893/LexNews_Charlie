namespace LexNews_Charlie.Models.ViewModels
{
    public class SubscriptionVM
    {
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
}
