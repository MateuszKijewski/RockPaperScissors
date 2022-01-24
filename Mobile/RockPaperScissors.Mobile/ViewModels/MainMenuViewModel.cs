using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel()
        {
            JoinGameCommand = new AsyncCommand(JoinGame);
            NewGameCommand = new AsyncCommand(NewGame);
            GameHistoryCommand = new AsyncCommand(GameHistory);
        }

        public AsyncCommand JoinGameCommand { get; set; }
        public AsyncCommand NewGameCommand { get; set; }
        public AsyncCommand GameHistoryCommand { get; set; }

        private async Task JoinGame()
        {

        }

        private async Task NewGame()
        {

        }
        private async Task GameHistory()
        {

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