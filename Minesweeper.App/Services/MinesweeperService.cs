using Microsoft.Extensions.Options;
using Minesweeper.App.Contracts;
using Minesweeper.App.ViewModels;
using System.Text;
using System.Text.Json;

namespace Minesweeper.App.Services
{
    public class MinesweeperService : IMinesweeperService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string _baseUrl;

        public MinesweeperService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _baseUrl = apiSettings.Value.BaseUrl;
        }
        public async Task<Guid> CreateGame(CreateGameRequest createGameRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/api/minesweeper")
                {
                    Content = new StringContent(JsonSerializer.Serialize(createGameRequest), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var gameId = JsonSerializer.Deserialize<Guid>(responseContent);

                    return gameId;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public async Task<GameStateViewModel> GetGameState(Guid gameId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/minesweeper/{gameId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var gameState = JsonSerializer.Deserialize<GameStateViewModel> (responseContent, _jsonOptions);

                return gameState;
            }

            return new GameStateViewModel();
        }
    }
}
