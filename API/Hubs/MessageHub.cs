using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class MessageHub : Hub
    {
        public static Dictionary<string, bool> clients = new();

        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("NEW_MSG", message);
        }

        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId, true);

            await Clients.Client(Context.ConnectionId).SendAsync("WELCOME", "Welcome all!");
            //return base.OnConnectedAsync();

            //return await Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (clients.Any(x => x.Key == Context.ConnectionId))
            {
                clients[Context.ConnectionId] = false;
            }

            return Task.CompletedTask;

            //return base.OnDisconnectedAsync(exception);
        }
    }
}
