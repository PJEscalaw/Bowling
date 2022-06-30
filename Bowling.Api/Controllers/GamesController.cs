using Business.DTOs.Games.Inputs;
using Business.Features.Games.Commands;
using Business.Features.Games.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.Api.Controllers
{
    public class GamesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateGamesAsync(CreateGamesInputDto createGamesDto)
            => Ok(await Mediator.Send(new CreateGamesCommand { Dto = createGamesDto }));

        [HttpPut]
        public async Task<IActionResult> UpdateGamesAsync(UpdateGamesInputDto updateGamesDto)
            => Ok(await Mediator.Send(new UpdateGamesCommand { Dto = updateGamesDto }));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGamesAsync(Guid Id)
            => Ok(await Mediator.Send(new DeleteGamesCommand { Id = Id }));

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetGamesByIdAsync(Guid Id)
            => Ok(await Mediator.Send(new GetGamesByIdQuery { Id = Id }));
    }
}
