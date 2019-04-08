using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Inheritance.Contracts.Recovery;
using Orleans;
using Orleans.Hosting;
using Orleans.Providers.Streams.AzureQueue;

namespace Inheritance.Client
{
    internal static class Program
    {
        private static async Task Main()
        {
            await Task.Delay(5000);
            var client = await StartClient();

            //await LaunchContractInheritanceTest(client);
            //await LaunchSoloTest(client);
            //await LaunchStormtrooperTest(client);
            //await LaunchDeathStarTest(client);
            //await LaunchStarDestroyerTest(client);

            await LaunchRecoverableTest(client);

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping client...");
        }

        private static async Task LaunchRecoverableTest(IClusterClient client)
        {
            Console.WriteLine("Init phase started...");
            const int idCount = 10;
            List<long> randomIdList = Enumerable.Range(1, idCount).Select(id => (long)id).ToList();

            foreach (var id in randomIdList)
            {
                var recoverableGrain = client.GetGrain<IRecoverableGrain>(id);
                await recoverableGrain.Init();
                var state = await recoverableGrain.ReadState();
                Console.WriteLine($"State of {recoverableGrain.GetPrimaryKeyLong().ToString()} is: {state.ToString()}");
            }

            Console.WriteLine("Init phase completed.");
            Console.WriteLine("Pausing for 5s...");
            await Task.Delay(5000);
            Console.WriteLine("Deactivation phase started...");
            
            foreach (var id in randomIdList)
            {
                var recoverableGrain = client.GetGrain<IRecoverableGrain>(id);
                await recoverableGrain.DeactivateExplicit();
            }

            Console.WriteLine("Deactivation phase completed.");
            Console.WriteLine("Pausing, waiting for keypress...");
            Console.ReadKey();
            //await Task.Delay(5000);
            
            Console.WriteLine("Recovery phase started...");
            var streamProvider = client.GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<RecoverSignal>(RecoverChannelId.Value, nameof(RecoverChannelId));
            await stream.OnNextAsync(RecoverSignal.Default);
            
            Console.WriteLine("Recovery phase completed.");
            Console.WriteLine("Pausing, waiting for keypress...");
            Console.ReadKey();
            Console.WriteLine("Final check phase started...");
            
            foreach (var id in randomIdList)
            {
                var recoverableGrain = client.GetGrain<IRecoverableGrain>(id);
                var state = await recoverableGrain.ReadState();
                Console.WriteLine($"State of {recoverableGrain.GetPrimaryKeyLong().ToString()} is: {state.ToString()}");
            }
            
            Console.WriteLine("Final check phase completed.");
        }

        private static async Task LaunchStarDestroyerTest(IClusterClient client)
        {
            var destroyer = client.GetGrain<IStarDestroyer>("Compellor");
            await destroyer.OpenGateway();
            
            var barrack = client.GetGrain<IBarrack>("DS-II, North bridge");
            await barrack.SendStormtroopers("Compellor");
        }

        private static async Task LaunchDeathStarTest(IClusterClient client)
        {
            var deathStar = client.GetGrain<IDeathStar>("DS-II");
            await deathStar.Build();
            await deathStar.Explode();
        }

        private static async Task LaunchStormtrooperTest(IClusterClient client)
        {
            var barrack = client.GetGrain<IBarrack>("DS-II, North bridge");
            IStormtrooper<Pistol> stormtrooperWithPistol1 = await barrack.GetStormtrooper<Pistol>("FN-666");
            await stormtrooperWithPistol1.Fire();
            IStormtrooper<Pistol> stormtrooperWithPistol2 = await barrack.GetStormtrooper<Pistol>("FN-667");
            await stormtrooperWithPistol2.Fire();
            IStormtrooper<Blaster> stormtrooperWithBlaster1 = await barrack.GetStormtrooper<Blaster>("FN-777");
            await stormtrooperWithBlaster1.Fire();
            IStormtrooper<Blaster> stormtrooperWithBlaster2 = await barrack.GetStormtrooper<Blaster>("FN-778");
            await stormtrooperWithBlaster2.Fire();
            await stormtrooperWithBlaster2.CallForReinforcement<Pistol>();
        }

        private static async Task LaunchSoloTest(IClusterClient client)
        {
            //var solo = client.GetGrain<ISolo>("Han"); // Expected to receive Exception: Cannot resolve grain interface ID=xxx to a grain class because of multiple implementations of it.

//            ISolo solo = client.GetGrain<IHanSolo>("Han");
//            await solo.Speak();
//            
//            solo = client.GetGrain<IFakeSolo>("Han");
//            await solo.Speak();

            IHanSolo hanSolo = client.GetGrain<IHanSolo>("Han and Chewie");
            await hanSolo.Speak();

            var chewie2 = hanSolo.AsReference<IChewie>();
            await chewie2.Roar();
            
            IChewie chewie = client.GetGrain<IChewie>("Han and Chewie");
            await chewie.Roar();
        }

        private static async Task LaunchContractInheritanceTest(IClusterClient client)
        {
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
        }
        
        private static async Task<IClusterClient> StartClient()
        {
            var client = new ClientBuilder()
                //.AddSimpleMessageStreamProvider(Streams.RadioStreamName)
                .UseLocalhostClustering()
                .AddAzureQueueStreams<AzureQueueDataAdapterV2>("aqs", optionsBuilder => optionsBuilder.Configure(options => { options.ConnectionString = "UseDevelopmentStorage=true"; }))
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }
    }
}