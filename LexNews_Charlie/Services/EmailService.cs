using LexNews_Charlie.Models;

namespace LexNews_Charlie.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("sendemail");
            _configuration = configuration;
        }
        public async Task<string> SendSubscriptionEmail(Email newEmail)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(_configuration["AzureRequestAddress"], newEmail);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return "some error ocurred";
            }
            return "Email will be sent";
        }
    }
}
