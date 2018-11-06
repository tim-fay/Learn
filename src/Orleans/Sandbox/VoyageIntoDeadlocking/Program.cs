using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;
using Orleans.Providers.Streams.AzureQueue;
using Orleans.Streams;
using VoyageIntoDeadlocking.Grains;

namespace VoyageIntoDeadlocking
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host = await StartSilo();
            var client = await StartClient();

            await LaunchStreamingBroadcast(client);

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping server...");
            await host.StopAsync();
        }

        private static async Task LaunchStreamingBroadcast(IClusterClient client)
        {
            await client.GetGrain<IAlienPlanet>("Venus").Discover();
            await client.GetGrain<IAlienPlanet>("Jupiter").Discover();
            await client.GetGrain<IAlienPlanet>("Mars").Discover();
            await client.GetGrain<IAlienPlanet>("Uranus").Discover();
            await client.GetGrain<IAlienPlanet>("Pluto").Discover();


            var planetEarthId = "Earth";
            var earthGrain = client.GetGrain<IRadioControl>(planetEarthId);
            var earthGrain2 = client.GetGrain<IRadioSource>(planetEarthId);
            await earthGrain.BroadcastMessage("Onward into the void!!");
            await earthGrain2.ReplyToSource("Reply to self...");
        }

        private static async Task<IClusterClient> StartClient()
        {
            var client = new ClientBuilder()
                //.AddSimpleMessageStreamProvider(Streams.RadioStreamName)
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>(Streams.RadioStreamName,
                    optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                //.azure
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
                //.AddSimpleMessageStreamProvider(Streams.RadioStreamName)
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>(Streams.RadioStreamName,
                    optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                .AddAzureTableGrainStorage("PubSubStore", options => options.ConnectionString = "UseDevelopmentStorage=true")
                //.AddMemoryGrainStorage("PubSubStore")
                .AddMemoryGrainStorageAsDefault()
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}