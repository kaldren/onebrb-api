using Microsoft.AspNetCore.SignalR;

namespace Onebrb.Infrastructure.Services;

public class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} joined the conversation");
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override Task OnDisconnectedAsync(System.Exception exception)
    {
        Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} left the conversation");
        return base.OnDisconnectedAsync(exception);
    }
}
