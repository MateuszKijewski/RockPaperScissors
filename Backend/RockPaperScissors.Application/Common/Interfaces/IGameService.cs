using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface IGameService
    {
        public Task<Game> StartGame(int scoreLimit);

        public Task<Game> JoinGame(Guid gameId);

        public Task MakeMove(Guid gameId, GameFigure figure);

        public Task StopGame(Guid gameId);
    }
}