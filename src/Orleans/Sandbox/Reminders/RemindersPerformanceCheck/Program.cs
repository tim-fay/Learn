using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;
using RemindersPerformanceCheck.Grains;

namespace RemindersPerformanceCheck
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host = await StartSilo();
            var client = await StartClient();

            await LaunchReminders(client, 1000, TimeSpan.FromSeconds(1));

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping server...");
            await host.StopAsync();
        }

        private static async Task LaunchReminders(IClusterClient client, int maxFireworks, TimeSpan delayBeforeLaunch)
        {
            var counter = client.GetGrain<IFireworkCounter>(0);
            
            var counterValue = await counter.ReadCurrentValue();
            Console.WriteLine($"Initial counter value: {counterValue}.");

            List<Task> fireworks = new List<Task>(maxFireworks);
            
            Console.WriteLine($"Initiating {maxFireworks} fireworks...");
            
            for (int fireworkNumber = 0; fireworkNumber < maxFireworks; fireworkNumber++)
            {
                var fireworkStarter = client.GetGrain<IFireworkStarter>(fireworkNumber);
                var fireworkIgnition = fireworkStarter.StartTimer(delayBeforeLaunch);
                fireworks.Add(fireworkIgnition);
            }
            
            Console.WriteLine($"Fireworks initiated and started!");
            Console.WriteLine("Waiting for fireworks...");

            await Task.WhenAll(fireworks);

            //counterValue = await counter.ReadCurrentValue();
            //Console.WriteLine($"See those {counterValue} fireworks in the sky!!!");            
        }

        private static async Task<IClusterClient> StartClient()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }


        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .AddMemoryGrainStorageAsDefault()
                .UseInMemoryReminderService()
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}