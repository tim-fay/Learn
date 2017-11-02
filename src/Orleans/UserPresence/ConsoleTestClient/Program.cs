using System;
using System.Net;
using System.Threading;

namespace ConsoleTestClient
{
    internal static class Program
    {
        private static void Main()
        {
            //ServicePointManager.UseNagleAlgorithm = true;
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.DefaultConnectionLimit = 1_000;
            Console.WriteLine("Press any key to start a test run...");
            Console.ReadLine();

            Console.WriteLine("Starting a conductor...");

            var testConductor = new TestConductor();

            Console.WriteLine("Running a test run...");

            //Run test clients
            var cancellationTokenSource = new CancellationTokenSource();
            var promise = testConductor.Run(cancellationTokenSource.Token);

            Console.WriteLine("Press Enter to abort...");
            Console.ReadLine();

            cancellationTokenSource.Cancel();
        }
    }
}