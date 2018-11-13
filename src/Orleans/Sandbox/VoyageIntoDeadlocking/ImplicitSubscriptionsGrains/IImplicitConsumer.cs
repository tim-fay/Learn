using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    public interface IImplicitConsumer : IGrainWithGuidKey
    {
        Task<bool> HasReceivedEvent();
    }
}