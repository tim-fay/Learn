using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    public class DataProducerGrain : Grain, IDataProducer
    {
        private IAsyncStream<IEvent> Stream { get; set; }
        
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(ImplicitConstants.ProviderName);
            Stream = streamProvider.GetStream<IEvent>(StreamIds.DataStreamId, ImplicitConstants.StreamNamespace);
            
            return base.OnActivateAsync();
        }

        public async Task PostData(IEvent @event)
        {
            await Stream.OnNextAsync(@event);
            //await Stream.OnNextAsync($"2-nd part of {text}");
            //await Stream.OnNextAsync($"3-rd part of {text}");

            //await Stream.OnErrorAsync(new Exception());
            //await Stream.OnCompletedAsync();
        }
    }
}