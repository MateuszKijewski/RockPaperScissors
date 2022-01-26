using RockPaperScissors.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors.Mobile.Common.Interfaces
{
    public interface IGameService
    {
        Task<Game> StartGame(int scoreLimit);

        Task<Game> JoinGame(string gameId);

        Task MakeMove(string gameId, GameFigure gameFigure);

        Task<IEnumerable<Game>> GetMyGames();

        Task<Game> FetchGame(string gameId);
    }
}