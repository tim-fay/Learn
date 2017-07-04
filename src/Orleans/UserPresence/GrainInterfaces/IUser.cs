using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IUser : IGrainWithGuidKey
    {
        Task Heartbeat();
        Task<bool> IsOnline();
    }
}
