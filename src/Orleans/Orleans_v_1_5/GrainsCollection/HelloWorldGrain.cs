using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace GrainsCollection
{
    public class HelloWorldGrain : Grain, IHelloWorld
    {
        public Task<string> SayHello(string helloMessageFrom)
        {
            return Task.FromResult($"Hello world message from {helloMessageFrom}");
        }
    }
}
