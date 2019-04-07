using Inheritance.Contracts.Recovery;
using Orleans.Streams;

namespace Inheritance.Server.Recovery
{
    public class RecoverableState
    {
        public StreamSubscriptionHandle<RecoverSignal> SubscriptionHandle { get; set; }
        public int Data { get; set; }
    }
}