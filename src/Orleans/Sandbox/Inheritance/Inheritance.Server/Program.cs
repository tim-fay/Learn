using System;
using System.Threading.Tasks;
using Orleans.Hosting;
using Orleans.Providers.Streams.AzureQueue;

namespace Inheritance.Server
{
    internal static class Program
    {
        private static async Task Main()
        {
            var host = await StartSilo();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping server...");
            await host.StopAsync();
        }
        
        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                //.AddSimpleMessageStreamProvider(Streams.RadioStreamName)
                .AddAzureTableGrainStorageAsDefault(options => options.ConnectionString = "UseDevelopmentStorage=true")
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>("aqs", optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                .AddAzureTableGrainStorage("PubSubStore", options => options.ConnectionString = "UseDevelopmentStorage=true")
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}