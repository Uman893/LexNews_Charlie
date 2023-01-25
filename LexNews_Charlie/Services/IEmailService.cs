using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public interface IEmailService
    {
        Task<string> SendSubscriptionEmail(Email newEmail);
    }
}
