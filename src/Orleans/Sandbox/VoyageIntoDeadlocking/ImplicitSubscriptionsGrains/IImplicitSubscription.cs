using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    public interface IImplicitSubscription : IGrainWithGuidKey
    {
        Task<bool> HasReceivedEvent();
    }
}