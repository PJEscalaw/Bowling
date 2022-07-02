using Business.DTOs.Scores.Inputs;
using Business.Features.Scores.Commands;
using Business.Features.Scores.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Api.Controllers
{
    public class ScoresController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateScoresAsync(CreateScoresInputDto createScoresDto)
            => Ok(await Mediator.Send(new CreateScoresCommand { Dto = createScoresDto }));

        [HttpPut]
        public async Task<IActionResult> UpdateScoresAsync(UpdateScoresInputDto updateScoresDto)
            => Ok(await Mediator.Send(new UpdateScoresCommand { Dto = updateScoresDto }));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteScoresAsync(Guid Id)
            => Ok(await Mediator.Send(new DeleteScoresCommand { Id = Id }));

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetScoresByIdAsync(Guid Id)
            => Ok(await Mediator.Send(new GetScoresByIdQuery { Id = Id }));

        [HttpGet]
        public async Task<IActionResult> GetScoresAsync()
            => Ok(await Mediator.Send(new GetScoresQuery()));

        [HttpGet("Games/{GameId}")]
        public async Task<IActionResult> GetScoresByGameIdAsync(Guid GameId)
            => Ok(await Mediator.Send(new GetScoresByGameIdQuery { GameId = GameId }));
    }
}
