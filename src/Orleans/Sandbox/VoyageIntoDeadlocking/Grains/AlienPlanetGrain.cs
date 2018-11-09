using System;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.Grains
{
    public class AlienPlanetGrain : Grain, IAlienPlanet
    {
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Streams.RadioStreamName);
            var stream = streamProvider.GetStream<BroadcastMessage>(Streams.RadioStreamId, Streams.RadioStreamNamespace);
            var subscriptions = await stream.GetAllSubscriptionHandles();
            if (subscriptions.Count == 0)
            {
                await stream.SubscribeAsync(RadioBroadcastHandler);
            }
            else
            {
                await subscriptions.Single().ResumeAsync(RadioBroadcastHandler);
            }
            
            await base.OnActivateAsync();

            async Task RadioBroadcastHandler(BroadcastMessage message, StreamSequenceToken _)
            {
                var source = message.Source;
                Console.WriteLine($"{DateTime.UtcNow}: Message received: '{message.Message}' from {source}");
                await Task.Delay(TimeSpan.FromSeconds(10));
                var radioSource = GrainFactory.GetGrain<IRadioSource>(source);
                await radioSource.ReplyToSource($"Hello out there from planet: {this.GetPrimaryKeyString()}!");
                Console.WriteLine($"{DateTime.UtcNow}: Replied to message...");
            }
        }

        public Task Discover()
        {
            Console.WriteLine($"Alien planet {this.GetPrimaryKeyString()} discovered.");
            return Task.CompletedTask;
        }
    }
}