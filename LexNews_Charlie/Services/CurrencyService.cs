using Azure.Core;
using Humanizer;
using LexNews_Charlie.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LexNews_Charlie.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        
        public CurrencyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetCurrency(string from, string to, decimal q) 
        {
            var client = new HttpClient();
            var apiAddress = (_configuration["RequestCurrency"]);
            var request = new HttpRequestMessage
              {
                  RequestUri = new Uri($"https://currency-exchange.p.rapidapi.com/exchange?from={from}&to={to}&q={q}"),
                  Headers =
                           {
                             { "X-RapidAPI-Key", _configuration["X-RapidAPI-Key"] },
                             { "X-RapidAPI-Host", _configuration["X-RapidAPI-Host"] },
                           },
            };

            var exchangeRate = " ";
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                exchangeRate = await response.Content.ReadAsStringAsync();                
            }

            var userQuery = exchangeRate;
            return (userQuery);
        }
    }
}

