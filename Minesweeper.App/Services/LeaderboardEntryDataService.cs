using Microsoft.Extensions.Options;
using Minesweeper.App.Contracts;
using Minesweeper.App.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Minesweeper.App.Services
{
    public class LeaderboardEntryDataService : ILeaderboardEntryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _baseUrl;

        public LeaderboardEntryDataService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _baseUrl = apiSettings.Value.BaseUrl;
        }

        public async Task<Guid> CreateLeaderboardEntry(LeaderboardViewModel leaderboardVm)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/leaderboardentry")
                {
                    Content = new StringContent(JsonSerializer.Serialize(leaderboardVm), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var courseId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return Guid.Empty;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                return Guid.Empty;
            }

            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public async Task<List<LeaderbordListViewModel>> GetLeaderbordList()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/LeaderboardEntry");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var leaderbordList = JsonSerializer.Deserialize<List<LeaderbordListViewModel>>(responseContent, _jsonOptions);

                return leaderbordList;
            }

            return new List<LeaderbordListViewModel>();
        }

        public async Task<List<LeaderbordListViewModel>> GetLeaderbordListByLevel(GameLevel gameLevel)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/LeaderboardEntry/{gameLevel}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var leaderbordList = JsonSerializer.Deserialize<List<LeaderbordListViewModel>>(responseContent, _jsonOptions);

                return leaderbordList;
            }

            return new List<LeaderbordListViewModel>();
        }
    }
}
