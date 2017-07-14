using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestClient
{
    public class TestClient
    {
        private readonly TimeSpan _waitDelay = TimeSpan.FromSeconds(2);

        private HttpClient HttpClient { get; }
        public long ClientId { get; }

        public TestClient(long clientId)
        {
            ClientId = clientId;
            HttpClient = new HttpClient();
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                //var response = await HttpClient.GetAsync($"http://localhost:54739/api/heartbeat/{ClientId}");
                var response = await HttpClient.GetAsync($"http://localhost:5000/api/heartbeat/{ClientId}");
                Console.WriteLine($"Heartbeat message sent from Client ID: {ClientId}, Response: {response.StatusCode}");
                await Task.Delay(_waitDelay, cancellationToken);
            }

        }
    }
}