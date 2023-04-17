using Microsoft.AspNetCore.SignalR;
using System.Net.Http;

namespace Demo.BL.Helper.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {

            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
