using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.Grains
{
    public interface IRadioControl : IGrainWithStringKey
    {
        Task BroadcastMessage(string message);
    }
}