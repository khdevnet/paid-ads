using System.Threading.Tasks;

namespace PaidAdsFeedFunctions.Storage
{
    public interface IStorageProvider
    {
        /// <summary>
        /// UploadFile uploads provided files stream to related container.
        /// </summary>
        /// <returns>Storage account host url</returns>
        Task<string> UploadFile(string fileName, string containerName, byte[] file, string contentType = null);
    }
}
