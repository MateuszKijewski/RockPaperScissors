using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Domain.Games
{
    public interface IGameSessionService
    {
        Guid? ResolveSession(Guid hostId, GameFigure hostFigure, Guid guestId, GameFigure guestFigure);
    }
}