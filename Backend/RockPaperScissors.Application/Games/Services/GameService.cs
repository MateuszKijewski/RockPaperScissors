using Dawn;
using Microsoft.AspNetCore.SignalR;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Application.Games.Hubs;
using RockPaperScissors.Domain.Entities;
using RockPaperScissors.Domain.Games;
using System.Transactions;

namespace RockPaperScissors.Application.Games.Services
{
    public class GameService : IGameService
    {
        private readonly IBaseRepositoryProvider _baseRepositoryProvider;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGameSessionService _gameSessionService;
        private readonly IHubContext<GameHub> _gameHub;

        public GameService(IBaseRepositoryProvider baseRepositoryProvider, ICurrentUserService currentUserService, IGameSessionService gameSessionService, IHubContext<GameHub> gameHub)
        {
            _baseRepositoryProvider = Guard.Argument(baseRepositoryProvider, nameof(baseRepositoryProvider)).NotNull().Value;
            _currentUserService = Guard.Argument(currentUserService, nameof(currentUserService)).NotNull().Value;
            _gameSessionService = Guard.Argument(gameSessionService, nameof(gameSessionService)).NotNull().Value;
            _gameHub = Guard.Argument(gameHub, nameof(gameHub)).NotNull().Value;
        }

        public async Task<Game> StartGame(int scoreLimit)
        {
            var gameRepository = _baseRepositoryProvider.GetRepository<Game>();
            var game = await gameRepository.AddAsync(new Game
            {
                HostScore = 0,
                GuestScore = 0,
                ScoreLimit = scoreLimit,
                IsActive = true,
                HostId = _currentUserService.Id
            });

            await _gameHub.Clients.Group(game.Id.ToString()).SendAsync("ReceiveGameUpdate", game);
            return game;
        }

        public async Task<Game> JoinGame(Guid gameId)
        {
            Game game;
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var gameRepository = _baseRepositoryProvider.GetRepository<Game>();
                game = gameRepository.GetAsync(gameId).GetAwaiter().GetResult();

                if (game.GuestId != null)
                {
                    throw new Exception("Game already has two players");
                }

                game.GuestId = _currentUserService.Id;
                await gameRepository.UpdateAsync(game);

                transaction.Complete();
            }

            await _gameHub.Clients.Group(gameId.ToString()).SendAsync("ReceiveGameUpdate", game);

            return game;
        }

        public async Task MakeMove(Guid gameId, GameFigure figure)
        {
            Game game;
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var gameRepository = _baseRepositoryProvider.GetRepository<Game>();
                game = await gameRepository.GetAsync(gameId);

                if (!game.IsActive)
                {
                    throw new Exception("This game is inactive");
                }

                var isHost = game.HostId == _currentUserService.Id;

                if (isHost && !game.HostTurnFinished)
                {
                    game.HostLastFigure = figure;
                    game.HostTurnFinished = true;
                }
                else if (!isHost && !game.GuestTurnFinished)
                {
                    game.GuestLastFigure = figure;
                    game.GuestTurnFinished = true;
                }

                if (game.HostTurnFinished && game.GuestTurnFinished)
                {
                    var winnerId = _gameSessionService.ResolveSession(game.HostId.Value, game.HostLastFigure, game.GuestId.Value, game.GuestLastFigure);
                    if (winnerId.HasValue)
                    {
                        var hostWonSession = winnerId == game.HostId;
                        if (hostWonSession)
                            game.HostScore++;
                        else
                            game.GuestScore++;
                    }
                }

                if (game.HostScore == game.ScoreLimit)
                    game.Result = GameResult.HostWin;
                else if (game.GuestScore == game.ScoreLimit)
                    game.Result = GameResult.GuestWin;

                if (game.Result.HasValue)
                    game.IsActive = false;

                await gameRepository.UpdateAsync(game);

                transaction.Complete();
            }

            var group = _gameHub.Clients.Group(gameId.ToString());
            await group.SendAsync("ReceiveGameUpdate", game);
        }

        public async Task StopGame(Guid gameId)
        {
            Game game;
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var gameRepository = _baseRepositoryProvider.GetRepository<Game>();
                game = await gameRepository.GetAsync(gameId);

                game.IsActive = false;

                await gameRepository.UpdateAsync(game);
                transaction.Complete();
            }

            await _gameHub.Clients.Group(gameId.ToString()).SendAsync("ReceiveGameUpdate", game);
        }

        public async Task<IEnumerable<Game>> GetMyGames()
        {
            var gameRepository = _baseRepositoryProvider.GetRepository<Game>();

            var userId = _currentUserService.Id;

            var games = await gameRepository.FindAsync(x => x.HostId == userId || x.GuestId == userId);

            return games;
        }
    }
}