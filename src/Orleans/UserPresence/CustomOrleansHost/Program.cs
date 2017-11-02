using System;
using System.Threading.Tasks;
using Grains;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;

namespace CustomOrleansHost
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    internal static class Program
    {
        private static async Task Main()
        {
            // First, configure and start a local silo
            ClusterConfiguration clusterConfiguration = ClusterConfiguration.LocalhostPrimarySilo();
            clusterConfiguration.AddMemoryStorageProvider();
            
            ISiloHost siloHost = new SiloHostBuilder()
                .UseConfiguration(clusterConfiguration)
                .AddApplicationPartsFromReferences(typeof(UserGrain).Assembly)
                .Build();

            
            await siloHost.StartAsync();
            Console.WriteLine("Silo started.");


            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            //client.Close();
            await siloHost.StopAsync();
        }
    }
}
