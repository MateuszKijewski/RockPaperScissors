using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Dtos.Contracts;
using RockPaperScissors.Mobile.Helpers;
using RockPaperScissors.Mobile.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private readonly IAuthService _authService;

        public LoginViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();

            LoginCommand = new Command(Login);
            SwitchToRegisterCommand = new Command(SwitchToRegister);
        }

        private async void SwitchToRegister()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(UserName)
                || string.IsNullOrEmpty(Password))
            {
                PopupHelper.DisplayMessage("Fields cannot be empty", "Incorrect data");
                return;
            }

            try
            {
                var response = await _authService.Login(new LoginContract
                {
                    Email = UserName,
                    Password = Password
                });
                if (response?.Token != null)
                {
                    await Shell.Current.GoToAsync(nameof(MainMenuPage));
                }
                else
                {
                    throw new Exception("Login error");
                }
            }
            catch (Exception ex)
            {
                PopupHelper.DisplayMessage(ex.Message, "Login error");
            }
        }

        private string _userName;
        private string _password;

        public string UserName
        {
            get => _userName;
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand SwitchToRegisterCommand { get; }
    }
}