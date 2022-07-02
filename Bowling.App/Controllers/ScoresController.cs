using Business.DTOs.Scores.Inputs;
using Facade.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bowling.App.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IScoresService _ScoresService;
        public ScoresController(IScoresService ScoresService) => _ScoresService = ScoresService;

        [HttpPost]
        public async Task<IActionResult> Post(CreateScoresInputDto input)
            => Ok(await _ScoresService.CreateScoresAsync(input));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateScoresInputDto input)
            => Ok(await _ScoresService.UpdateScoresAsync(input));

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid input)
            => Ok(await _ScoresService.DeleteScoresAsync(input));

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _ScoresService.GetScoresAsync());
    }
}
