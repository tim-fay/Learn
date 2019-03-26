using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    [ImplicitStreamSubscription(ImplicitConstants.StreamNamespace)]
    public class ImplicitConsumerGrain : Grain, IImplicitConsumer
    {
        private bool ReceivedEvent { get; set; }
        
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(ImplicitConstants.ProviderName);
            var stream = streamProvider.GetStream<IEvent>(StreamIds.DataStreamId, ImplicitConstants.StreamNamespace);

            await stream.SubscribeAsync((@event, _) =>
                {
                    ReceivedEvent = true;
                    Console.WriteLine($"Next event received: '{@event.What}'");
                    return Task.CompletedTask;
                },
                exception =>
                {
                    Console.WriteLine($"Stream error: {exception.Message}");
                    return Task.CompletedTask;
                }, 
                () =>
                {
                    Console.WriteLine("Stream completed");
                    return Task.CompletedTask;
                });

            await base.OnActivateAsync();
        }

        public Task<bool> HasReceivedEvent()
        {
            return Task.FromResult(ReceivedEvent);
        }
    }
}