using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IDeathStar : IGrainWithStringKey
    {
        Task Build();
        Task Explode();
    }
}