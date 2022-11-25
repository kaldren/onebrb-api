namespace Onebrb.Infrastructure.Interfaces;

public interface INotificationsService
{
    Task Notify(string userId, string message, string method);
}
