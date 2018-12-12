using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public class EarthGrain : Grain, IRadioControl, IRadioSource
    {
        private IAsyncStream<BroadcastMessage> RadioStream { get; set; }
        private IAsyncStream<string> RadioStream2 { get; set; }
        
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(ExplicitConstants.RadioStreamName);
            RadioStream = streamProvider.GetStream<BroadcastMessage>(ExplicitConstants.RadioStreamId, ExplicitConstants.RadioStreamNamespace);

            var allSubscriptionHandles = await RadioStream.GetAllSubscriptionHandles();
            //RadioStream2 = streamProvider.GetStream<string>(ExplicitConstants.RadioStreamId, ExplicitConstants.RadioStreamNamespace);
            await base.OnActivateAsync();
        }

        public async Task BroadcastMessage(string message)
        {
            Console.WriteLine($"Start broadcasting");
            var broadcastMessage = ExplicitSubscriptionsGrains.BroadcastMessage.New("Message into the void!", this.GetPrimaryKeyString());
            await RadioStream.OnNextAsync(broadcastMessage);
            //await RadioStream2.OnNextAsync("message");
            Console.WriteLine($"Stop broadcasting");
        }

        public Task ReplyToSource(string replyMessage)
        {
            Console.WriteLine($"Received reply message: '{replyMessage}' from outer space.");
            return Task.CompletedTask;
        }
    }
}