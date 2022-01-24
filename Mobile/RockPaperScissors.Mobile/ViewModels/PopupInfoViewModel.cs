using MvvmHelpers.Commands;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Mobile.ViewModels
{
    public class PopupInfoViewModel
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                if (value != _message)
                {
                    _message = value;
                }
            }
        }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                if (value != _title)
                {
                    _title = value;
                }
            }
        }

        public AsyncCommand ClosePopupCommand { get; }

        public PopupInfoViewModel()
        {
            ClosePopupCommand = new AsyncCommand(ClosePopup);
        }

        private async Task ClosePopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}