using RockPaperScissors.Application.Games.Hubs;

namespace RockPaperScissors.WebApi.Helpers
{
    public static class ConfigurationHelper
    {
        public static string[] GetAllowedOrigins(IConfiguration configuration)
        {
            string hosts = configuration.GetValue<string>("AllowedOrigins");
            return hosts.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void ConfigureSignalR(this WebApplication webApplication)
        {
            webApplication.UseEndpoints(routes => routes.MapHub<GameHub>("/ws/game"));
        }
    }
}
