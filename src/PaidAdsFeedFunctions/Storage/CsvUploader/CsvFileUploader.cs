using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PaidAdsFeedFunctions.Configuration;
using PaidAdsFeedFunctions.DAL;
using PaidAdsFeedFunctions.Mappers;

namespace PaidAdsFeedFunctions.Storage.CsvUploader
{
    public class CsvFileUploader : ICsvFileUploader
    {
        private readonly FeedCountryDetailsOptions _countryAddressOptions;
        private readonly IStorageProvider _storageProvider;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOptions<FrontendOptions> _frontendOptions;

        public CsvFileUploader(
            IConfiguration configuration,
            IStorageProvider storageProvider,
            IVehicleRepository vehicleRepository,
            IOptions<FrontendOptions> frontendOptions)
        {
            _countryAddressOptions = configuration.
                ExtractApplicationSpecificOptions<FeedCountryDetailsOptions>(FeedCountryDetailsOptions.SectionKey);
            _storageProvider = storageProvider;
            _vehicleRepository = vehicleRepository;
            _frontendOptions = frontendOptions;
        }

        public async Task<string> UploadFacebookFeed(string fileName, string containerName)
        {
            var vehicles = await _vehicleRepository.GetVehicles();

            var feeds = vehicles.Select(v => v.ToFacebookFeed(_frontendOptions.Value, _countryAddressOptions));

            return await UploadFeed(feeds, fileName, containerName);
        }

        public async Task<string> UploadGoogleFeed(string fileName, string containerName)
        {
            var vehicles = await _vehicleRepository.GetVehicles();

            var feeds = vehicles.Select(v => v.ToGoogleFeed(_frontendOptions.Value, _countryAddressOptions));

            return await UploadFeed(feeds, fileName, containerName);
        }

        public async Task<string> UploadInstagramFeed(string fileName, string containerName)
        {
            var vehicles = await _vehicleRepository.GetVehicles();

            var feeds = vehicles.Select(v => v.ToInstagramFeed(_frontendOptions.Value, _countryAddressOptions));

            return await UploadFeed(feeds, fileName, containerName);
        }

        private async Task<string> UploadFeed<TFeed>(IEnumerable<TFeed> feeds, string fileName, string containerName) where TFeed : class
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    csvWriter.WriteRecords(feeds);

                var bytes = memoryStream.ToArray();

                return await _storageProvider.UploadFile(fileName, containerName, bytes, "text/csv");
            }
        }
    }
}
