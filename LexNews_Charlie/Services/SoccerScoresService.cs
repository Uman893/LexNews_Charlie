using LexNews_Charlie.Models.ViewModels;

namespace LexNews_Charlie.Services
{
    public class SoccerScoresService : ISoccerScoresService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SoccerScoresService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<DisplayScoresVM> GetScores(string sport)
        {
            using var client = new HttpClient();
            var apikey = _configuration["Live-Scores-Key"];
            var response = await client.GetAsync($"https://api.the-odds-api.com/v4/sports/soccer_uefa_europa_league/scores/?daysFrom=1&apiKey=c5144b1a5a799fb452c978e9ba3a3355");//to modify parameter to change markets 
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DisplayScoresVM>();
        }

    }
}
