using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;
using Orleans.Runtime;
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
            siloConfig.Defaults.DefaultTraceLevel = Severity.Error;
            var silo = new SiloHost("TestSilo", siloConfig);
            silo.InitializeOrleansSilo();
            silo.StartOrleansSilo();

            Console.WriteLine("Silo started.");

            // Then configure and connect a client.
            var clientConfig = ClientConfiguration.LocalhostSilo();
            //clientConfig.DefaultTraceLevel = Severity.Error;
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

            await fibonacciClient.CalcNumber(10);
            await fibonacciClient.CalcNumber(20);
            await fibonacciClient.CalcNumber(1000);
            await fibonacciClient.CalcNumber(1001);
            await fibonacciClient.CalcNumber(1002);

            stopWatch.Stop();
            Console.WriteLine($"Fibonacci calculations ended... Overall time taken: {stopWatch.ElapsedMilliseconds}");
        }

    }

    internal class FibonacciClient
    {
        private IClusterClient Client { get; }

        public FibonacciClient(IClusterClient client)
        {
            Client = client;
        }

        public async Task CalcNumber(long index)
        {
            Console.Write($"Calculating Number of index: {index}...   ");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IFibonacciNumber fibonacciNumber = Client.GetGrain<IFibonacciNumber>(index);
            var number = await fibonacciNumber.CalculateNumber();
            stopwatch.Stop();
            Console.WriteLine($"Number of index 100 is: {number}, Calculation time (ms) taken: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
