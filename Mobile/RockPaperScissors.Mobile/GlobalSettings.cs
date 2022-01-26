namespace RockPaperScissors.Mobile
{
    public class GlobalSettings
    {
        private readonly static string _baseUrl = @"http://192.168.0.22:5182";
        private readonly string _defaultAuthEndpoint = $@"{_baseUrl}/api/Auth";
        private readonly string _defaultGameEndpoint = $@"{_baseUrl}/api/Game";
        private readonly string _gameWebSocketEndpoint = $@"{_baseUrl}/ws/game";

        public static GlobalSettings Instance { get; } = new GlobalSettings();

        public string DefaultAuthEndpoint
        { get { return _defaultAuthEndpoint; } }

        public string DefaultGameEndpoint
        { get { return _defaultGameEndpoint; } }

        public string GameWebSocketEndpoint
        { get { return _gameWebSocketEndpoint; } }
    }
}