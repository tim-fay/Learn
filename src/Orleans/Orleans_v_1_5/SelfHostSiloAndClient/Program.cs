using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace SelfHostSiloAndClient
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // First, configure and start a local silo
            var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
            var silo = new SiloHost("TestSilo", siloConfig);
            silo.InitializeOrleansSilo();
            silo.StartOrleansSilo();

            Console.WriteLine("Silo started.");

            // Then configure and connect a client.
            var clientConfig = ClientConfiguration.LocalhostSilo();
            var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
            client.Connect().Wait();

            Console.WriteLine("Client connected.");

            //
            // This is the place for your test code.
            //
            IHelloWorld helloWorld = client.GetGrain<IHelloWorld>(Guid.Empty);
            Console.WriteLine(helloWorld.SayHello("Bill Gates").Result);

            DoFibonacciCalculations(client).Wait();


            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            client.Close();
            silo.ShutdownOrleansSilo();
        }

        private static async Task DoFibonacciCalculations(IClusterClient clusterClient)
        {
            Console.WriteLine("Fibonacci calculations started...");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var fibonacciClient = new FibonacciClient(clusterClient);

            var num10 = await fibonacciClient.GetNumber(10);
            Console.WriteLine($"Number of index 10 is: {num10}");
            var num100 = await fibonacciClient.GetNumber(20);
            Console.WriteLine($"Number of index 100 is: {num100}");
            var num1000 = await fibonacciClient.GetNumber(1000);
            Console.WriteLine($"Number of index 1000 is: {num1000}");
            //clusterClient.Logger.

            stopWatch.Stop();
            Console.WriteLine($"Fibonacci calculations ended... Time taken: {stopWatch.ElapsedMilliseconds}");
        }

    }

    internal class FibonacciClient
    {
        private IClusterClient Client { get; }

        public FibonacciClient(IClusterClient client)
        {
            Client = client;
        }

        public async Task<string> GetNumber(long index)
        {
            IFibonacciNumber fibonacciNumber = Client.GetGrain<IFibonacciNumber>(index);
            var number = await fibonacciNumber.CalculateNumber();
            return number.ToString();
        }
    }
}
