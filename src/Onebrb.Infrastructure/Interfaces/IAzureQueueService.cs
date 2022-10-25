namespace Onebrb.Infrastructure.Interfaces;

public interface IAzureQueueService
{
    Task<bool> InsertAsync(string queue, string message);
}
