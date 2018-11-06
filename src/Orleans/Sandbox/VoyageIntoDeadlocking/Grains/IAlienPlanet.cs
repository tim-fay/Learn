using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.Grains
{
    public interface IAlienPlanet : IGrainWithStringKey
    {
        Task Discover();
    }
}