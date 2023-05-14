using CsharpBot.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CsharpBot.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ChatOptions> _options;

        public OpenAIService(HttpClient httpClient, IOptions<ChatOptions> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<Message> CreateChatCompletion(List<Message> messages) 
        {
            var request = new { model = _options.Value.GtpModel, messages = messages.ToArray() };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Value.ApiKey);

            var response = await _httpClient.PostAsJsonAsync(_options.Value.ApiUrl, request);
            response.EnsureSuccessStatusCode();

            var chatCompletionResponse = await response.Content.ReadFromJsonAsync<ChatbotResponse>();
            return chatCompletionResponse?.choices.First().message;
        }
    }
}
