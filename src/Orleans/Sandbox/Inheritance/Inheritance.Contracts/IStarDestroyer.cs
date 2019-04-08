using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IStarDestroyer : IGrainWithStringKey
    {
        Task OpenGateway();
    }
}