using Microsoft.AspNetCore.Http;

namespace Onebrb.Infrastructure.Interfaces
{
    public interface IAzureImageStoreService
    {
        Task StoreImageAsync(string imageName, string containerName, IFormFile file);

        bool IsImage(byte[] file);
    }
}
