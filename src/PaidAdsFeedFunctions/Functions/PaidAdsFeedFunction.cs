using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaidAdsFeedFunctions.Storage.CsvUploader;

namespace PaidAdsFeedFunctions.Functions
{
    public class PaidAdsFeedFunction
    {
        private readonly string _carsForAdsFileName;
        private readonly string _carsForInstagramAdsFileName;
        private readonly string _carsForGoogleAdsFileName;
        private readonly string _paidAdsContainerName;
        private readonly ICsvFileUploader _csvFileUploader;

        public PaidAdsFeedFunction(
            ICsvFileUploader csvFileUploader,
            IConfiguration configuration)
        {
            _csvFileUploader = csvFileUploader;
            _paidAdsContainerName = configuration.GetValue<string>("PaidAdsFeedFunctions:BlobStorage:ContainerName", "paid-ads-feed");
            _carsForAdsFileName = configuration.GetValue<string>("PaidAdsFeedFunctions:BlobStorage:CarsForAdsFileName", "cars-for-ads.csv");
            _carsForInstagramAdsFileName = configuration.GetValue<string>("PaidAdsFeedFunctions:BlobStorage:CarsForInstagramAdsFileName", "cars-for-instagram-ads.csv");
            _carsForGoogleAdsFileName = configuration.GetValue<string>("PaidAdsFeedFunctions:BlobStorage:CarsForGoogleAdsFileName", "cars-for-google-ads.csv");
        }

        [Function("PaidAdsFeedFunction")]
        public async Task Run([TimerTrigger("0 */30 * * * *", RunOnStartup = true)] FunctionContext context)
        {
            var log = context.GetLogger<PaidAdsFeedFunction>();

            log.LogInformation($"Function PaidAdsFeedFunction executed at: {DateTime.Now}");

            var facebookUrlTask = _csvFileUploader.UploadFacebookFeed(_carsForAdsFileName, _paidAdsContainerName);
            var instagramUrlTask = _csvFileUploader.UploadInstagramFeed(_carsForInstagramAdsFileName, _paidAdsContainerName);
            var googleUrlTask = _csvFileUploader.UploadGoogleFeed(_carsForGoogleAdsFileName, _paidAdsContainerName);

            await Task.WhenAll(facebookUrlTask, instagramUrlTask, googleUrlTask);

            var facebookUrl = await facebookUrlTask;
            var instagramUrl = await instagramUrlTask;
            var googleUrl = await googleUrlTask;

            log.LogInformation($"Facebook feed link: {facebookUrl} \r\nInstagram feed link: {instagramUrl} \r\nGoogle feed link: {googleUrl}");
        }
    }
}
