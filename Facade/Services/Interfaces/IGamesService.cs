using Business.DTOs.Games.Inputs;
using Business.DTOs.Games.Outputs;

namespace Facade.Services.Interfaces
{
    public interface IGamesService
    {
        Task<CreateGamesOutputDto> CreateGameAsync(CreateGamesInputDto createGamesDto);
        Task<bool> DeleteGameAsync(Guid id);
        Task<UpdateGamesOutputDto> UpdateGameAsync(UpdateGamesInputDto updateGamesDto);
    }
}
