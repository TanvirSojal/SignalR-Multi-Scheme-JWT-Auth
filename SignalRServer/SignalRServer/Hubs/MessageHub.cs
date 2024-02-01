using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.Models;

namespace SignalRServer.Hubs
{
    public class MessageHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        [Authorize(AuthenticationSchemes = AuthSchemes.WORLD_SCHEME)]
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
