using Business.DTOs.Scores.Inputs;
using Business.DTOs.Scores.Outputs;

namespace Facade.Services.Interfaces
{
    public interface IScoresService
    {
        Task<CreateScoresOutputDto> CreateScoresAsync(CreateScoresInputDto createScoresDto);
        Task<bool> DeleteScoresAsync(Guid id);
        Task<IEnumerable<GetScoresOutputDto>> GetScoresAsync();
        Task<UpdateScoresOutputDto> UpdateScoresAsync(UpdateScoresInputDto updateScoresDto);
    }
}
