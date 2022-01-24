using RockPaperScissors.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockPaperScissors.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameHistoryPage : ContentPage
    {
        public GameHistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as GameHistoryViewModel).FetchMyGamesCommand.ExecuteAsync();
        }
    }
}