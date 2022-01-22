using RockPaperScissors.Domain.Entities;

namespace RockPaperScissors.Domain.Games
{
    public class GameSessionService : IGameSessionService
    {
        public Guid? ResolveSession(Guid hostId, GameFigure hostFigure, Guid guestId, GameFigure guestFigure)
        {
            if (hostFigure == guestFigure)
                return null;
            if (hostFigure == GameFigure.Rock && guestFigure == GameFigure.Paper)
                return guestId;
            if (hostFigure == GameFigure.Rock && guestFigure == GameFigure.Scissors)
                return hostId;
            if (hostFigure == GameFigure.Paper && guestFigure == GameFigure.Rock)
                return hostId;
            if (hostFigure == GameFigure.Paper && guestFigure == GameFigure.Scissors)
                return guestId;
            if (hostFigure == GameFigure.Scissors && guestFigure == GameFigure.Rock)
                return guestId;
            if (hostFigure == GameFigure.Scissors && guestFigure == GameFigure.Paper)
                return hostId;

            return null;
        }
    }
}