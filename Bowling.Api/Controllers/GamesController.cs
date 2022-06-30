using Business.DTOs.Games.Inputs;
using Business.Features.Games.Commands;
using Business.Features.Games.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Api.Controllers
{
    public class GamesController : BaseController
    {
        [HttpPost("Games")]
        public async Task<IActionResult> CreateGamesAsync(CreateGamesDto createGamesDto)
            => Ok(await Mediator.Send(new CreateGamesCommand { Dto = createGamesDto }));

        [HttpPut("Games")]
        public async Task<IActionResult> UpdateGamesAsync(UpdateGamesDto updateGamesDto)
            => Ok(await Mediator.Send(new UpdateGamesCommand { Dto = updateGamesDto }));

        [HttpDelete("Games/{Id}")]
        public async Task<IActionResult> DeleteGamesAsync(Guid Id)
            => Ok(await Mediator.Send(new DeleteGamesCommand { Id = Id }));

        [HttpGet("Games/{Id}")]
        public async Task<IActionResult> GetGamesByIdAsync(Guid Id)
            => Ok(await Mediator.Send(new GetGamesByIdQuery { Id = Id }));
    }
}
