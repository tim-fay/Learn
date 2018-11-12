using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public interface IRadioControl : IGrainWithStringKey
    {
        Task BroadcastMessage(string message);
    }
}