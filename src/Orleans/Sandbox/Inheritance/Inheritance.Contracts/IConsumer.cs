using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IConsumer : IGrainWithStringKey
    {
        Task Consume(IUser user);
    }
}