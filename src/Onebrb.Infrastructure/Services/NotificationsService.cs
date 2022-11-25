using Microsoft.AspNetCore.SignalR;
using Onebrb.Infrastructure.Interfaces;

namespace Onebrb.Infrastructure.Services;

public class NotificationsService : INotificationsService
{
    private readonly IHubContext<NotificationsHub> _hubContext;

    public NotificationsService(IHubContext<NotificationsHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Notify(string userId, string message, string method = "ToastNotification")
    {
        var users = _hubContext.Clients.Users(userId);

        await users.SendAsync(method, message);
    }
}
