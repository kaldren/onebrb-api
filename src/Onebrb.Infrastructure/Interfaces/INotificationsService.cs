namespace Onebrb.Infrastructure.Interfaces;

public interface INotificationsService
{
    Task Notify(string connectionId);
}
