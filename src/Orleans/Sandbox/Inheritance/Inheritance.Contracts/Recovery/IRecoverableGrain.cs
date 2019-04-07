using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts.Recovery
{
    public interface IRecoverableGrain : IGrainWithIntegerKey
    {
        Task Init();

        Task DeactivateExplicit();

        Task<int> ReadState();
    }
}