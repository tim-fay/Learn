using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface ISuperUser : IUser, IGrainWithIntegerKey
    {
        Task DoSuper();
    }
}