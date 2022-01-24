using RockPaperScissors.Mobile.Common.Interfaces;

namespace RockPaperScissors.Mobile.Services
{
    public class SettingsService : ISettingsService
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (value == null)
                    return;

                _userName = value;
            }
        }

        private string _accessToken;

        public string AccessToken
        {
            get => _accessToken;
            set
            {
                if (value == null)
                    return;

                _accessToken = value;
            }
        }
    }
}