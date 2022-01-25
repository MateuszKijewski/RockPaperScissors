using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.Services
{
    public class GameService : IGameService
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly ISettingsService _settingsService;

        public GameService()
        {
            _settingsService = DependencyService.Get<ISettingsService>();
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<IEnumerable<Game>> GetMyGames()
        {
            var client = CreateHttpClient();
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _settingsService.AccessToken);


            var response = await client.GetAsync($"{GlobalSettings.Instance.DefaultGameEndpoint}/GetMyGames");

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Game>>(stringResponse);
            }
            throw new Exception("Joining game failed");
        }

        public async Task<Game> JoinGame(string gameId)
        {
            var client = CreateHttpClient();
            var content = new StringContent("");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync($"{GlobalSettings.Instance.DefaultGameEndpoint}/JoinGame/{gameId}", content);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Game>(stringResponse);
            }
            throw new Exception("Joining game failed");
        }

        public async Task MakeMove(string gameId, GameFigure gameFigure)
        {
            var client = CreateHttpClient();
            var content = new StringContent(gameFigure.ToString());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync($"{GlobalSettings.Instance.DefaultGameEndpoint}/MakeMove/{gameId}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Making move failed");
            }
        }

        public async Task<Game> StartGame(int scoreLimit)
        {
            var client = CreateHttpClient();
            var content = new StringContent(scoreLimit.ToString());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync($"{GlobalSettings.Instance.DefaultGameEndpoint}/StartGame", content);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Game>(stringResponse);
            }
            throw new Exception("Starting game failed");
        }

        private HttpClient CreateHttpClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}