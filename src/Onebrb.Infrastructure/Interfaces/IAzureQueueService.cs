namespace Onebrb.Infrastructure.Interfaces;

public interface IAzureQueueService
{
    Task<bool> InsertAsync(string queue, string message);
}

//public interface INotifyNewCommentService : IAzureQueueService
//{
//    Task<bool> NotifyNewComment(string queue, string message);
//}