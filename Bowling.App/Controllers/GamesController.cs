using Business.DTOs.Games.Inputs;
using Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.App.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGamesService _gamesService;
        public GamesController(IGamesService gamesService) => _gamesService = gamesService;

        [HttpPost]
        public async Task<IActionResult> Post(CreateGamesInputDto input)
            => Ok(await _gamesService.CreateGameAsync(input));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateGamesInputDto input)
            => Ok(await _gamesService.UpdateGameAsync(input));

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid input)
            => Ok(await _gamesService.DeleteGameAsync(input));

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _gamesService.GetGameAsync());
    }
}
