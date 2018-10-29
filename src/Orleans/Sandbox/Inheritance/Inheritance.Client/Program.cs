using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Client
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var client = await StartClient();

            await LaunchContractInheritanceTest(client);


            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

            Console.WriteLine("Stopping client...");
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
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }
    }
}