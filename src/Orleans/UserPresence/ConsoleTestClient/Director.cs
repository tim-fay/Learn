using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestClient
{
    public class TestConductor
    {
        private const int MaxClientsCount = 10_000;

        private List<TestClient> TestClients { get; }

        public TestConductor()
        {
            TestClients = PrepareTestClients();
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            List<Task> promises = new List<Task>(MaxClientsCount);

            foreach (var client in TestClients)
            {
                Console.WriteLine($"Running client, Id: {client.ClientId}");
                promises.Add(client.Run(cancellationToken));
            }

            await Task.WhenAll(promises);
        }

        private List<TestClient> PrepareTestClients()
        {
            var testClients = new List<TestClient>(MaxClientsCount);

            for (long i = 1; i <= MaxClientsCount; i++)
            {
                var client = new TestClient(i);
                testClients.Add(client);
            }

            return testClients;
        }
    }
}