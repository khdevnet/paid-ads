using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace PaidAdsFeedFunctions.Storage
{
    public class StorageProvider : IStorageProvider
    {
        private readonly IOptions<BlobStorageOptions> _blobStorageOptions;
        private readonly BlobServiceClient _blobServiceClient;

        public StorageProvider(IOptions<BlobStorageOptions> blobStorageOptions)
        {
            _blobStorageOptions = blobStorageOptions;
            _blobServiceClient = new BlobServiceClient(_blobStorageOptions.Value.PublicConnectionString);
        }

        public async Task<string> UploadFile(string fileName, string containerName, byte[] file, string contentType = null)
        {
            var container = await GetContainer(containerName);
            var blockBlob = container.GetBlobClient(fileName);

            var blobHttpHeaders = string.IsNullOrEmpty(contentType) ? null : new BlobHttpHeaders { ContentType = contentType };

            using (var stream = new MemoryStream(file, writable: false))
            {
                await blockBlob.UploadAsync(stream, blobHttpHeaders);
            }

            return blockBlob.Uri.AbsoluteUri;
        }

        private async Task<BlobContainerClient> GetContainer(string containerName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);

            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            return container;
        }
    }
}
