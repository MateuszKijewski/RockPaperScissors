using MvvmHelpers.Commands;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Helpers;
using RockPaperScissors.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class GameHistoryViewModel : BindableObject
    {
        private readonly IGameService _gameService;

        public GameHistoryViewModel()
        {
            FetchMyGamesCommand = new AsyncCommand(FetchMyGames);

            _gameService = DependencyService.Get<IGameService>();
        }

        public AsyncCommand FetchMyGamesCommand { get; set; }

        private async Task FetchMyGames()
        {
            try
            {
                var games = await _gameService.GetMyGames();
                _myGames = games.Select(x => new GameDetail
                {
                    Title = x.Result == GameResult.HostWin
                        ? $"{x.Host.FirstName} {x.Host.LastName}"
                        : $"{x.Guest.FirstName} {x.Guest.LastName}",
                    Details = $"{x.HostScore} - {x.GuestScore}"
                });
            }
            catch (Exception ex)
            {
                PopupHelper.DisplayMessage(ex.Message, "Fetching my games error");
            }
        }

        private IEnumerable<GameDetail> _myGames;
        public IEnumerable<GameDetail> MyGames
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

    public class GameDetail
    {
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
