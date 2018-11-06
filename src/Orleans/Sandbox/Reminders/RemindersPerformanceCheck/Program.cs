using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using RemindersPerformanceCheck.Grains;

namespace RemindersPerformanceCheck
{
    internal static class Program
    {
        private static async Task Main()
        {
            var host = await StartSilo();
            var client = await StartClient();

            await LaunchReminders(client, 100, TimeSpan.FromSeconds(20));

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
            
            Console.WriteLine($"{DateTime.Now}: Fireworks initiated and started!");
            await Task.WhenAll(fireworks);
            
            Console.WriteLine($"{DateTime.Now}: Waiting for fireworks...");


            //counterValue = await counter.ReadCurrentValue();
            //Console.WriteLine($"See those {counterValue} fireworks in the sky!!!");            
        }

        private static async Task<IClusterClient> StartClient()
        {
            var client = new ClientBuilder()
                //.UseAzureStorageClustering(options => options.ConnectionString = "UseDevelopmentStorage=true")
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
//                .Configure<ClusterOptions>(options =>
//                {
//                    options.ClusterId = "ClarityOrleansClusterId";
//                    options.ServiceId = "ClarityOrleansServiceId";
//                })
                .UseAdoNetReminderService(options =>
                {
                    options.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Orleans;Integrated Security=True;Pooling=False;Max Pool Size=200;MultipleActiveResultSets=True";
                    options.Invariant = "System.Data.SqlClient";
                })
//                .UseAzureTableReminderService(options =>
//                {
//                    options.ConnectionString = "UseDevelopmentStorage=true";
//                })
                //.UseInMemoryReminderService()
//                .UseAzureStorageClustering(options =>
//                {
//                    options.ConnectionString = "";
//                });
                //.AddMemoryGrainStorageAsDefault()
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}