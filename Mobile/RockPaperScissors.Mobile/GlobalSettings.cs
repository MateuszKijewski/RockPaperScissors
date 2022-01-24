namespace RockPaperScissors.Mobile
{
    public class GlobalSettings
    {
        private readonly string _defaultAuthEndpoint = @"http://192.168.0.22:5182/api/Auth";
        private readonly string _defaultGameEndpoint = @"http://192.168.0.22:5182/api/Game";
        private readonly string _gameWebSocketEndpoint = @"http://192.168.0.22:5182/ws/game";

        public static GlobalSettings Instance { get; } = new GlobalSettings();

        public string DefaultAuthEndpoint
        { get { return _defaultAuthEndpoint; } }

        public string DefaultGameEndpoint
        { get { return _defaultGameEndpoint; } }

        public string GameWebSocketEndpoint
        { get { return _gameWebSocketEndpoint; } }
    }
}