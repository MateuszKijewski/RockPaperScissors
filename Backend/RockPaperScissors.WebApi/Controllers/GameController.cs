using Dawn;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.WebApi.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = Guard.Argument(gameService, nameof(gameService)).NotNull().Value;
        }

        [HttpPost]
        [Route("api/[controller]/StartGame")]
        public async Task<ActionResult> StartGame([FromBody] int scoreLimit)
        {
            try
            {
                var game = await _gameService.StartGame(scoreLimit);

                return Ok(game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/JoinGame/{gameId}")]
        public async Task<ActionResult> JoinGame([FromRoute] string gameId)
        {
            try
            {
                var game = await _gameService.JoinGame(new Guid(gameId));

                return Ok(game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/MakeMove/{gameId}")]
        public async Task<ActionResult> MakeMove([FromRoute] string gameId, [FromBody] GameFigure gameFigure)
        {
            try
            {
                await _gameService.MakeMove(new Guid(gameId), gameFigure);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/StopGame/{gameId}")]
        public async Task<ActionResult> StopGame([FromRoute] string gameId)
        {
            try
            {
                await _gameService.StopGame(new Guid(gameId));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
