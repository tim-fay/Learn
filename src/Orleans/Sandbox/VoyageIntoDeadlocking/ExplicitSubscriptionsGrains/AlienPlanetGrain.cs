using System;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public class AlienPlanetGrain : Grain, IAlienPlanet, IAsyncObserver<BroadcastMessage>
    {
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(ExplicitConstants.RadioStreamName);
            var stream = streamProvider.GetStream<BroadcastMessage>(ExplicitConstants.RadioStreamId, ExplicitConstants.RadioStreamNamespace);
            var subscriptions = await stream.GetAllSubscriptionHandles();
            if (subscriptions.Count == 0)
            {
                var streamSubscriptionHandle = await stream.SubscribeAsync(this);
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

        public Task OnNextAsync(BroadcastMessage item, StreamSequenceToken token = null)
        {
            throw new NotImplementedException();
        }

        public Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}