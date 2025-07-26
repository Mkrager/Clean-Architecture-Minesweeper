using Minesweeper.App.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Minesweeper.App.Services
{
    public class MinesweeperService : IMinesweeperService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MinesweeperService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }


        public async Task<Guid> CreateSmallGame()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7171/api/Minesweeper/small-game");

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
    }
}
