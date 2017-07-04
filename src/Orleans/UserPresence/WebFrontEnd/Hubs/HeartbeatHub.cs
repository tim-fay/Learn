using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WebFrontEnd.Hubs
{
    public class HeartbeatHub : Hub
    {
        public override Task OnConnected()
        {
            // Ping Orleans Grain here

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (!stopCalled)
            {
                // Ping Orleans Grain here
            }

            return base.OnDisconnected(stopCalled);
        }

        public Task PingAsync()
        {
            // Ping Orleans Grain here

            return Task.CompletedTask;
        }
    }
}