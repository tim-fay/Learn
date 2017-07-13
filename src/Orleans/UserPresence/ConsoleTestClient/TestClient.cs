using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleTestClient
{
    public class TestClient
    {
        private HttpClient HttpClient { get; }
        private long ClientId { get; }

        public TestClient(long clientId)
        {
            ClientId = clientId;
            HttpClient = new HttpClient();
            //httpClient.BaseAddress = 
        }

        public Task Run()
        {
            //http://localhost:54739/api/values
            //HttpClient.GetAsync("")

            return Task.CompletedTask;
        }
    }
}