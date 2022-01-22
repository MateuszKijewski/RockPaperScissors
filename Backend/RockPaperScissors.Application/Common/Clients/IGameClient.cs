using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Application.Common.Clients
{
    public interface IGameClient
    {
        Task ReceiveGameUpdate(Game game);
    }
}