using Business.DTOs.Scores.Inputs;
using Business.DTOs.Scores.Outputs;
using Facade.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Facade.Services.Scores
{
    public class ScoresService : IScoresService
    {
        private readonly HttpClient _client;
        public ScoresService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("bowling.api");
            _client.BaseAddress = new Uri("https://localhost:7108/");
        }

        public async Task<CreateScoresOutputDto> CreateScoresAsync(CreateScoresInputDto createScoresDto)
        {
            var result = await _client.PostAsJsonAsync("api/Scores", createScoresDto);
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CreateScoresOutputDto>(jsonResponse);
        }

        public async Task<bool> DeleteScoresAsync(Guid id)
        {
            var deleteUrl = $"api/Scores/{id}";
            var result = await _client.DeleteAsync(deleteUrl);

            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GetScoresOutputDto>> GetScoresAsync()
        {
            var result = await _client.GetAsync("api/Scores");
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<GetScoresOutputDto>>(jsonResponse);
        }

        public async Task<UpdateScoresOutputDto> UpdateScoresAsync(UpdateScoresInputDto updateScoresDto)
        {
            var result = await _client.PutAsJsonAsync("api/Scores", updateScoresDto);
            var jsonResponse = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UpdateScoresOutputDto>(jsonResponse);
        }
    }
}
