using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Services;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton<ISettingsService>(new SettingsService());
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.Register<IGameService, GameService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
