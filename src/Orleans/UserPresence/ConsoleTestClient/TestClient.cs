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
        private long ClientId { get; }

        public TestClient(long clientId)
        {
            ClientId = clientId;
            HttpClient = new HttpClient();
            //httpClient.BaseAddress = 
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var response = await HttpClient.GetAsync($"http://localhost:54739/api/heartbeat?id={ClientId}");
                await Task.Delay(_waitDelay);
            }

        }
    }
}