using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public interface IAlienPlanet : IGrainWithStringKey
    {
        Task Discover();
    }
}