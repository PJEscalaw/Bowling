using Business.DTOs.Games.Inputs;
using Business.DTOs.Games.Outputs;
using Facade.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Facade.Services.Games
{
    public class GamesService : IGamesService
    {
        private readonly HttpClient _client;

        public GamesService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("bowling.api");
            _client.BaseAddress = new Uri("https://localhost:7108/");
        }

        public async Task<CreateGamesOutputDto> CreateGameAsync(CreateGamesInputDto createGamesDto)
        {
            var result = await _client.PostAsJsonAsync("api/Games", createGamesDto);
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CreateGamesOutputDto>(jsonResponse);
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            var deleteUrl = $"api/Games/{id}";
            var result = await _client.DeleteAsync(deleteUrl);

            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GetGamesOutputDto>> GetGameAsync()
        {
            var result = await _client.GetAsync("api/Games");
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<GetGamesOutputDto>>(jsonResponse);
        }

        public async Task<UpdateGamesOutputDto> UpdateGameAsync(UpdateGamesInputDto updateGamesDto)
        {
            var result = await _client.PutAsJsonAsync("api/Games", updateGamesDto);
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UpdateGamesOutputDto>(jsonResponse);
        }
    }
}
