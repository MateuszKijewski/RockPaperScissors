using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers.Commands;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Helpers;
using RockPaperScissors.Mobile.Models;
using RockPaperScissors.Mobile.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class GameViewModel : BindableObject
    {
        private readonly IGameService _gameService;
        private readonly ISettingsService _settingsService;

        public GameViewModel()
        {
            MakeMoveWithRockCommand = new AsyncCommand(MakeMoveWithRock);
            MakeMoveWithPaperCommand = new AsyncCommand(MakeMoveWithPaper);
            MakeMoveWithScissorsCommand = new AsyncCommand(MakeMoveWithScissors);
            StopHubConnectionCommand = new AsyncCommand(StopHubConnection);

            _settingsService = DependencyService.Get<ISettingsService>();
            _gameService = DependencyService.Get<IGameService>();
        }

        public AsyncCommand MakeMoveWithRockCommand { get; set; }
        public AsyncCommand MakeMoveWithPaperCommand { get; set; }
        public AsyncCommand MakeMoveWithScissorsCommand { get; set; }
        public AsyncCommand StopHubConnectionCommand { get; set; }

        public async void Instantiate()
        {
            Game = await _gameService.FetchGame(GameId);

            await StartHubConnection();
            _hubConnection.On<Game>("ReceiveGameUpdate", (game) =>
            {
                Game = game;
                if (game.Result.HasValue)
                {
                    PopupHelper.DisplayMessage("Game finished", $"{game.Result}");
                    Shell.Current.GoToAsync($"//{nameof(MainMenuPage)}");
                }
            });
        }

        private async Task StartHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{GlobalSettings.Instance.GameWebSocketEndpoint}?token={_gameId}", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_settingsService.AccessToken);
                })
                .Build();

            await _hubConnection.StartAsync();
        }

        private async Task StopHubConnection()
        {
            await _hubConnection.StopAsync();
        }

        private async Task MakeMoveWithRock()
        {
            await MakeMove(GameFigure.Rock);
        }

        private async Task MakeMoveWithPaper()
        {
            await MakeMove(GameFigure.Paper);
        }

        private async Task MakeMoveWithScissors()
        {
            await MakeMove(GameFigure.Scissors);
        }

        private async Task MakeMove(GameFigure gameFigure)
        {
            try
            {
                await _gameService.MakeMove(_gameId, gameFigure);
            }
            catch (Exception ex)
            {
                PopupHelper.DisplayMessage(ex.Message, "Making move error");
            }
        }

        private HubConnection _hubConnection { get; set; }

        private string _gameId;

        public string GameId
        {
            get => _gameId;
            set
            {
                if (value != _gameId)
                {
                    _gameId = value;
                    OnPropertyChanged();
                }
            }
        }

        private Game _game = new Game();

        public Game Game
        {
            get => _game;
            set
            {
                if (value != _game)
                {
                    _game = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}