using MvvmHelpers.Commands;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Helpers;
using RockPaperScissors.Mobile.Models;
using System;
using System.Collections.ObjectModel;
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
                MyGames = new ObservableCollection<GameDetail>(games.Select(x => new GameDetail
                {
                    Title = x.Result == GameResult.HostWin
                        ? $"(Winner) {x.Host.FirstName} {x.Host.LastName} vs {x.Guest.FirstName} {x.Guest.LastName}"
                        : $"{x.Host.FirstName} {x.Host.LastName} vs (Winner) {x.Guest.FirstName} {x.Guest.LastName}",
                    Details = $"{x.HostScore} - {x.GuestScore}"
                }));
            }
            catch (Exception ex)
            {
                PopupHelper.DisplayMessage(ex.Message, "Fetching my games error");
            }
        }

        private ObservableCollection<GameDetail> _myGames = new ObservableCollection<GameDetail>();
        public ObservableCollection<GameDetail> MyGames
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
