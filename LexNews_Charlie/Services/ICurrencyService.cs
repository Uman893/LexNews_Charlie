using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public interface ICurrencyService
    {       
        Task<string> GetCurrency(string from, string to, decimal q);
    }
}



