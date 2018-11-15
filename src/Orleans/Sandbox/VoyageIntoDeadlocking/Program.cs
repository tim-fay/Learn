using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;
using Orleans.Providers.Streams.AzureQueue;
using VoyageIntoDeadlocking.ExplicitSubscriptionsGrains;
using VoyageIntoDeadlocking.ImplicitSubscriptionsGrains;

namespace VoyageIntoDeadlocking
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host = await StartSilo();
            var client = await StartClient();

            //await LaunchImplicitStreamingExample(client);
            //await LaunchStreamingBroadcast(client);
            await LaunchInterfaceStatedGrain(client);
            
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping server...");
            await host.StopAsync();
        }

        private static async Task LaunchInterfaceStatedGrain(IClusterClient client)
        {
            var richStateGrain = client.GetGrain<IRichStateGrain>("AAA");
            
            var data = await richStateGrain.ReadState();
            Console.WriteLine($"Reading state: {data}");
            
            await richStateGrain.SaveState("text 1", new DateTime(2010, 1, 1), 42);

            data = await richStateGrain.ReadState();
            Console.WriteLine($"Reading state: {data}");

            await richStateGrain.SaveState(RichData.New("text 2", new DateTime(2020, 2, 2), 43));

            data = await richStateGrain.ReadState();
            Console.WriteLine($"Reading state: {data}");

        }


        private static async Task LaunchImplicitStreamingExample(IClusterClient client)
        {
            var producer = client.GetGrain<IDataProducer>("Producer");

            await producer.PostData("Produced data #1");

            await Task.Delay(TimeSpan.FromSeconds(5));
            var result = await client.GetGrain<IImplicitConsumer>(Guid.Empty).HasReceivedEvent();
            Console.WriteLine($"Has received {result}");
            
//            await producer.PostData("Produced data #2");
//
//            await Task.Delay(TimeSpan.FromSeconds(5));
//            result = await client.GetGrain<IImplicitConsumer>(Guid.Empty).HasReceivedEvent();
//            Console.WriteLine($"Has received {result}");
            
            //for (int i = 0; i < 10; i++)
            {
                //await producer.PostData($"Produced data #{i}");
            }
        }

        private static async Task LaunchStreamingBroadcast(IClusterClient client)
        {
            await client.GetGrain<IAlienPlanet>("Venus").Discover();
            await client.GetGrain<IAlienPlanet>("Jupiter").Discover();
            await client.GetGrain<IAlienPlanet>("Mars").Discover();
            await client.GetGrain<IAlienPlanet>("Mercury").Discover();
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
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>(ExplicitConstants.RadioStreamName, optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>(ImplicitConstants.ProviderName, optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
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
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>(ExplicitConstants.RadioStreamName, optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>(ImplicitConstants.ProviderName, optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                .AddAzureTableGrainStorage("PubSubStore", options => options.ConnectionString = "UseDevelopmentStorage=true")
                //.AddMemoryGrainStorage("PubSubStore")
                .AddAzureBlobGrainStorageAsDefault(options => options.ConnectionString = "UseDevelopmentStorage=true")
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}