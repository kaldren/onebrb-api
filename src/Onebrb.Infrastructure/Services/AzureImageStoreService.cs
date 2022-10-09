using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Onebrb.Infrastructure.Interfaces;

namespace Onebrb.Infrastructure.Services
{
    public class AzureImageStoreService : IAzureImageStoreService
    {
        private readonly IConfiguration _configuration;
        BlobServiceClient _blobServiceClient;
        string _jpegFormat = "jpeg";

        public AzureImageStoreService(IConfiguration configuration)
        {
            _configuration = configuration;

            _blobServiceClient = new BlobServiceClient(_configuration["Onebrb:Azure:StorageAccount:ConnectionString"]);
        }

        public bool IsImage(byte[] file)
        {
            var headers = new List<byte[]>
            {
                //Encoding.ASCII.GetBytes("BM"),      // BMP
                //Encoding.ASCII.GetBytes("GIF"),     // GIF
                //new byte[] { 137, 80, 78, 71 },     // PNG
                //new byte[] { 73, 73, 42 },          // TIFF
                //new byte[] { 77, 77, 42 },          // TIFF
                new byte[] { 255, 216, 255, 224 },  // JPEG
                new byte[] { 255, 216, 255, 225 }   // JPEG CANON
            };

            return headers.Any(x => x.SequenceEqual(file.Take(x.Length)));
        }

        public async Task StoreImageAsync(string imageName, string containerName, IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                byte[] bytes = ms.ToArray();

                if (!IsImage(bytes))
                    throw new InvalidDataException("The file is not a JPEG.");
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            if (!await containerClient.ExistsAsync())
                await _blobServiceClient.CreateBlobContainerAsync(containerName);

            var uniqueFileName = $"{imageName}_{Path.GetRandomFileName()}.{_jpegFormat}";

            using (var stream = file.OpenReadStream())
            {
                var result = await containerClient.UploadBlobAsync(uniqueFileName, stream);
            }
        }
    }
}