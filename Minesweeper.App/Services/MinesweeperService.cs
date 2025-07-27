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
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7171/api/minesweeper/small-game");

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

        public async Task<OpenCellVm> OpenCell(OpenCellRequest openCellRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7171/api/minesweeper/open-cell")
                {
                    Content = new StringContent(JsonSerializer.Serialize(openCellRequest), Encoding.UTF8, "application/json")
                };


                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var openState = JsonSerializer.Deserialize<OpenCellVm>(responseContent, _jsonOptions);

                    return openState;
                }
                return new OpenCellVm();
            }
            catch (Exception ex)
            {
                return new OpenCellVm();
            }
        }

        public async Task<ToggleFlagVm> ToggleFlag(ToggleFlagRequest toggleFlagRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7171/api/minesweeper/toggle-flag")
                {
                    Content = new StringContent(JsonSerializer.Serialize(toggleFlagRequest), Encoding.UTF8, "application/json")
                };


                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var toggleFlag = JsonSerializer.Deserialize<ToggleFlagVm>(responseContent, _jsonOptions);

                    return toggleFlag;
                }
                return new ToggleFlagVm();
            }
            catch (Exception ex)
            {
                return new ToggleFlagVm();
            }
        }
        public async Task<GameStateViewModel> GetGameState(Guid gameId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7171/api/minesweeper/{gameId}");

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
