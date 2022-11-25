using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Onebrb.Infrastructure.Services;

[Authorize]
public class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId(this.Context);

        await Clients.All.SendAsync("ToastNotification", new
        {
            text = $"Welcome {userId}",
            username = GetUserId(this.Context)
        });

    }

    public async Task SendMessageToUser(string userId, string message)
    {
        await Clients.User(userId).SendAsync(message);
    }


    public override Task OnDisconnectedAsync(System.Exception exception)
    {
        Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} left the conversation");
        return base.OnDisconnectedAsync(exception);
    }

    //public async Task SendMessage(string user, string message)
    //{
    //    await Clients.All.SendAsync("ReceiveMessage", user, message);
    //}

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", new
        {
            text = message,
            username = GetUserId(this.Context)
        });
    }

    private string GetUserId(HubCallerContext context)
    {
        return context.User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.microsoft.com/identity/claims/objectidentifier", System.StringComparison.InvariantCultureIgnoreCase)).Value;
    }
}

[Authorize]
public class IdBasedUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
    }
}