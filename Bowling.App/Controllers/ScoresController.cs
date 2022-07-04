using Business.DTOs.Scores.Inputs;
using Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.App.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IScoresService _scoresService;
        public ScoresController(IScoresService ScoresService) => _scoresService = ScoresService;

        [HttpPost]
        public async Task<IActionResult> Post(CreateScoresInputDto input)
            => Ok(await _scoresService.CreateScoresAsync(input));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateScoresInputDto input)
            => Ok(await _scoresService.UpdateScoresAsync(input));

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid input)
            => Ok(await _scoresService.DeleteScoresAsync(input));

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _scoresService.GetScoresAsync());

        [HttpGet("Games")]
        public async Task<IActionResult> GetScoresByGamesIdAsync(Guid gameId)
            => Ok(await _scoresService.GetScoresByGameIdAsync(gameId));

        [HttpDelete("Games")]
        public async Task<IActionResult> DeleteScoresByGamesIdAsync(Guid gameId)
         => Ok(await _scoresService.DeleteScoresByGameIdAsync(gameId));
    }
}
