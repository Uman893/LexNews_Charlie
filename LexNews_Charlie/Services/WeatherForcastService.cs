using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public class WeatherForcastService : IWeaterForcastService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public WeatherForcastService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherApi> GetWeather(string City)
        {
            using var client = new HttpClient();            
            var apikey = _configuration["OpenWeather"];
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?&units=metric&q={City}&appid={apikey}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<WeatherApi>();
        } 
    }
}



