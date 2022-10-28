using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Onebrb.Infrastructure.Interfaces;

namespace Onebrb.Infrastructure.Services;

public class AzureQueueService : IAzureQueueService
{
    private readonly IConfiguration _configuration;
    private QueueClient? _queueClient;

    public AzureQueueService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> InsertAsync(string queue, string message)
    {
        try
        {
            string storageCS = _configuration["Onebrb:Azure:StorageAccount:ConnectionString"];
            _queueClient = new QueueClient(storageCS, queue, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            //await _queueClient.CreateIfNotExistsAsync();

            _queueClient.SendMessage(message);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
