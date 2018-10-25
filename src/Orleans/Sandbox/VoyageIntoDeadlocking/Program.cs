using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Hosting;
using VoyageIntoDeadlocking.Grains;

namespace VoyageIntoDeadlocking
{
    internal static class Program
    {
       
        private static async Task Main(string[] args)
        {
            var host = await StartSilo();
            var client = await StartClient();

            //await LaunchStreamingBroadcast(client);

            ISuperUser user42 = client.GetGrain<ISuperUser>(42);
            IUser user43 = client.GetGrain<ISuperUser>(43);
            //IUser user = client.GetGrain<ISuperUser>(42);
            await user42.DoSuper();
            await user43.DoSimple();

            var consumer555 = client.GetGrain<IConsumer>("Consumer 555");
            var consumer777 = client.GetGrain<IConsumer>("Consumer 777");

            await consumer555.Consume(user42);
            await consumer555.Consume(user43);

            await consumer777.Consume(user42);
            await consumer777.Consume(user43);
            

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping server...");
            await host.StopAsync();
        }

        private static async Task LaunchStreamingBroadcast(IClusterClient client)
        {
            await client.GetGrain<IAlienPlanet>(Guid.NewGuid()).Create();
            await client.GetGrain<IAlienPlanet>(Guid.NewGuid()).Create();
            await client.GetGrain<IAlienPlanet>(Guid.NewGuid()).Create();
            await client.GetGrain<IAlienPlanet>(Guid.NewGuid()).Create();
            await client.GetGrain<IAlienPlanet>(Guid.NewGuid()).Create();
            await client.GetGrain<IAlienPlanet>(Guid.NewGuid()).Create();


            var planetEarthId = Guid.Empty;
            var earthGrain = client.GetGrain<IRadioControl>(planetEarthId);
            await earthGrain.BroadcastMessage("Onwards into the void!!");
        }

        private static async Task<IClusterClient> StartClient()
        {
            var client = new ClientBuilder()
                .AddSimpleMessageStreamProvider(Streams.RadioStreamName)
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
                .AddSimpleMessageStreamProvider(Streams.RadioStreamName)
                .AddMemoryGrainStorageAsDefault()
                .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}