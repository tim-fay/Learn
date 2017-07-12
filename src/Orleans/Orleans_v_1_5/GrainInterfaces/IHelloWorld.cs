using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IHelloWorld : IGrainWithGuidKey
    {
        Task<string> SayHello(string helloMessageFrom);
    }
}
