using RockPaperScissors.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockPaperScissors.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(GameId), "gameId")]

    public partial class GamePage : ContentPage
    {
        public string GameId
        {
            set
            {
                (BindingContext as GameViewModel).GameId = value;
            }
        }

        public GamePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as GameViewModel).Instantiate();
        }

        protected override async void OnDisappearing()
        {
            await (BindingContext as GameViewModel).StopHubConnectionCommand.ExecuteAsync();
            base.OnDisappearing();
        }
    }
}