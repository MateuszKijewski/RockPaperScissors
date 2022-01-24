using MvvmHelpers.Commands;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Helpers;
using RockPaperScissors.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class GameHistoryViewModel : BindableObject
    {
        private readonly IGameService _gameService;

        public GameHistoryViewModel(IGameService gameService)
        {
            FetchMyGamesCommand = new AsyncCommand(FetchMyGames);

            _gameService = gameService;
        }

        public AsyncCommand FetchMyGamesCommand { get; set; }

        private async Task FetchMyGames()
        {
            try
            {
                _myGames = await _gameService.GetMyGames();
            }
            catch (Exception ex)
            {
                PopupHelper.DisplayMessage(ex.Message, "Fetching my games error");
            }
        }

        private IEnumerable<Game> _myGames;
        public IEnumerable<Game> MyGames
        {
            get => _myGames;
            set
            {
                if (value != _myGames)
                {
                    _myGames = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
