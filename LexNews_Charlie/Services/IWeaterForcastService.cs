using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public interface IWeaterForcastService
    {
        Task<WeatherApi> GetWeather(string city);
    }
}
