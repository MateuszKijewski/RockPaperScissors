using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Dtos.Contracts;
using RockPaperScissors.Mobile.Helpers;
using RockPaperScissors.Mobile.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        private readonly IAuthService _authService;

        public RegisterViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();

            RegisterCommand = new Command(Register);
            SwitchToLoginCommand = new Command(SwitchToLogin);
        }

        private async void SwitchToLogin(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        private async void Register(object obj)
        {
            if (string.IsNullOrEmpty(UserName)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(ConfirmPassword)
                || string.IsNullOrEmpty(FirstName)
                || string.IsNullOrEmpty(LastName))
            {
                PopupHelper.DisplayMessage("Fields cannot be empty", "Incorrect data");
                return;
            }

            try
            {
                await _authService.Register(new RegisterContract
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Password = Password,
                    Email = UserName
                });

                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                PopupHelper.DisplayMessage(ex.Message, "Registration error");
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand SwitchToLoginCommand { get; }

        private string _userName;
        private string _password;
        private string _confirmPassword;
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != _firstName)
                {
                    _firstName = value;
                    OnPropertyChanged(UserName);
                }
            }

        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged(UserName);
                }
            }

        }

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (value != _confirmPassword)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
