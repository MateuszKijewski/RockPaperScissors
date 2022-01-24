using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RockPaperScissors.Mobile.Common.Interfaces;
using RockPaperScissors.Mobile.Dtos.Contracts;
using RockPaperScissors.Mobile.Dtos.Responses;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockPaperScissors.Mobile.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISettingsService _settingsService;
        private readonly JsonSerializerSettings _serializerSettings;

        public AuthService()
        {
            _settingsService = DependencyService.Get<ISettingsService>();
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<LoginResponse> Login(LoginContract loginContract)
        {
            var client = CreateHttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(loginContract, _serializerSettings));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync($"{GlobalSettings.Instance.DefaultAuthEndpoint}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var convertedResponse = JsonConvert.DeserializeObject<LoginResponse>(stringResponse);

                _settingsService.AccessToken = convertedResponse.Token;
                _settingsService.UserName = loginContract.Email;

                return convertedResponse;
            }
            throw new Exception("Login failed");
        }

        public async Task<RegisterResponse> Register(RegisterContract registerContract)
        {
            var client = CreateHttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(registerContract, _serializerSettings));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync($"{GlobalSettings.Instance.DefaultAuthEndpoint}/register", content);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var convertedResponse = JsonConvert.DeserializeObject<RegisterResponse>(stringResponse);

                return convertedResponse;
            }
            throw new Exception("Registration failed");
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