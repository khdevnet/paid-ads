using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaidAdsFeedFunctions.DAL;
using PaidAdsFeedFunctions.Storage;
using PaidAdsFeedFunctions.Storage.CsvUploader;

namespace PaidAdsFeedFunctions
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            var hostingEnv = provider.GetService<IWebHostEnvironment>();

            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddSingleton<IStorageProvider, StorageProvider>();
            services.AddSingleton<ICsvFileUploader, CsvFileUploader>();
        }
    }
}
