using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PaidAdsFeedFunctions
{
    public class Program
    {
        public static Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureAppConfiguration()
                .ConfigureServices(new Startup().ConfigureServices)
                .Build();

            return host.RunAsync();
        }
    }
}
