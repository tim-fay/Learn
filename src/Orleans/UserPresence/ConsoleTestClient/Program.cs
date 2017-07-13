using System;
using System.Threading;

namespace ConsoleTestClient
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Press any key to start a test run...");
            Console.Read();

            Console.WriteLine("Starting a conductor...");

            var testConductor = new TestConductor();

            Console.WriteLine("Running a test run...");

            //Run test clients
            var cancellationTokenSource = new CancellationTokenSource();
            var promise = testConductor.Run(cancellationTokenSource.Token);

            Console.WriteLine("Press Enter to abort...");
            Console.ReadLine();

            cancellationTokenSource.Cancel();

            promise.Wait();

            //Console.ReadLine();
        }
    }
}