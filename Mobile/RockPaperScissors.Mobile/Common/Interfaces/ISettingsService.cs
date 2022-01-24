namespace RockPaperScissors.Mobile.Common.Interfaces
{
    public interface ISettingsService
    {
        string UserName { get; set; }

        string AccessToken { get; set; }
    }
}