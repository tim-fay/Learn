using System;
using System.Threading.Tasks;
using Orleans.Hosting;

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
                .AddMemoryGrainStorageAsDefault()
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}