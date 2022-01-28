using System.Threading.Tasks;

namespace PaidAdsFeedFunctions.Storage.CsvUploader
{
    public interface ICsvFileUploader
    {
        Task<string> UploadFacebookFeed(string fileName, string containerName);
        Task<string> UploadInstagramFeed(string fileName, string containerName);
        Task<string> UploadGoogleFeed(string fileName, string containerName);
    }
}
