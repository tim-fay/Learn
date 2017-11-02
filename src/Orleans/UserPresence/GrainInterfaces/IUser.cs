using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IUser : IGrainWithIntegerKey
    {
        Task Heartbeat();
        Task<bool> IsOnline();
    }
}
