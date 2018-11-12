using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    public interface IDataProducer : IGrainWithStringKey
    {
        Task PostData(string text);
    }
}