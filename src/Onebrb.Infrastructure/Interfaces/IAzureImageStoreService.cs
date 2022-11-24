namespace Onebrb.Infrastructure.Interfaces
{
    public interface IAzureImageStoreService
    {
        Task StoreImageAsync(string imageName, string containerName, byte[] file);

        bool IsImage(byte[] file);
    }
}
