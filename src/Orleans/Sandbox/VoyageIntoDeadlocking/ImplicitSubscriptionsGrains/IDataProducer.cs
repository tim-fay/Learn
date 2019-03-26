using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    public interface IDataProducer : IGrainWithStringKey
    {
        Task PostData(IEvent @event);
    }
}