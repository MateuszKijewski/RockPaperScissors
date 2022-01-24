using MvvmHelpers;
using MvvmHelpers.Commands;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel()
        {
            JoinGameCommand = new AsyncCommand(JoinGame);
            NewGameCommand = new AsyncCommand(NewGame);
            ShowGameHistoryCommand = new AsyncCommand(ShowGameHistory);
        }

        public AsyncCommand JoinGameCommand { get; set; }
        public AsyncCommand NewGameCommand { get; set; }
        public AsyncCommand ShowGameHistoryCommand { get; set; }

        private async Task JoinGame()
        {

        }

        private async Task NewGame()
        {

        }
        private async Task ShowGameHistory()
        {
            await Shell.Current.GoToAsync($"//{nameof(GameHistoryPage)}");
        }

        private string _scoreLimit;
        public string ScoreLimit
        {
            get => _scoreLimit;
            set
            {
                if (value != _scoreLimit)
                {
                    _scoreLimit = value;
                    OnPropertyChanged();
                }
            }
        }

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
    }
}