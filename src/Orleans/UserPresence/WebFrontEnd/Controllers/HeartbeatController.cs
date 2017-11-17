using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace WebFrontEnd.Controllers
{
    [Route("api/[controller]")]
    public class HeartbeatController : Controller
    {
        private IClusterClient ClusterClient { get; }

        public HeartbeatController(IClusterClient clusterClient)
        {
            ClusterClient = clusterClient;
        }

        [HttpGet("{id}")]
        public async Task Get(long id)
        {
            var user = ClusterClient.GetGrain<IUser>(id);
            await user.Heartbeat();
        }

    }
}