using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IUser : IGrainWithIntegerKey
    {
        Task DoSimple();
    }
}