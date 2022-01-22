using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using RockPaperScissors.Application.Common.Clients;

namespace RockPaperScissors.Application.Games.Hubs
{
    public class GameHub : Hub<IGameClient>
    {


        public override async Task OnConnectedAsync()
        {
            var httpContext = ((IHttpContextFeature)Context.Features[typeof(IHttpContextFeature)]).HttpContext;
            var token = httpContext.Request.Query["token"].First();

            await Groups.AddToGroupAsync(Context.ConnectionId, token);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = ((IHttpContextFeature)Context.Features[typeof(IHttpContextFeature)]).HttpContext;
            var token = httpContext.Request.Query["token"].First();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, token);

            await base.OnDisconnectedAsync(exception);
        }
    }
}