using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public class EarthGrain : Grain, IRadioControl, IRadioSource
    {
        private IAsyncStream<BroadcastMessage> RadioStream { get; set; }
        
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(ExplicitConstants.RadioStreamName);
            RadioStream = streamProvider.GetStream<BroadcastMessage>(ExplicitConstants.RadioStreamId, ExplicitConstants.RadioStreamNamespace);
            return base.OnActivateAsync();
        }

        public async Task BroadcastMessage(string message)
        {
            Console.WriteLine($"Start broadcasting");
            var broadcastMessage = ExplicitSubscriptionsGrains.BroadcastMessage.New("Message into the void!", this.GetPrimaryKeyString());
            await RadioStream.OnNextAsync(broadcastMessage);
            Console.WriteLine($"Stop broadcasting");
        }

        public Task ReplyToSource(string replyMessage)
        {
            Console.WriteLine($"Received reply message: '{replyMessage}' from outer space.");
            return Task.CompletedTask;
        }
    }
}