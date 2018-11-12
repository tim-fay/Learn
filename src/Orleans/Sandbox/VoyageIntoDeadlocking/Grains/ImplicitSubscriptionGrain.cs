using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.Grains
{
    public class Namespaces
    {
        public const string ProviderName = "Stream";
        public const string StreamNamespace = "Provider";
    }

    public static class StreamIds
    {
        public static readonly Guid DataStreamId = Guid.Parse("00000000-2CA3-48DC-8AF3-211A8A59539D");
    }

    public interface IImplicitSubscription : IGrainWithGuidKey
    {
        Task<bool> HasReceivedEvent();
    }
    
    [ImplicitStreamSubscription(Namespaces.StreamNamespace)]
    public class ImplicitSubscriptionGrain : Grain, IImplicitSubscription
    {
        private bool ReceivedEvent { get; set; }
        
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Namespaces.ProviderName);
            var stream = streamProvider.GetStream<string>(StreamIds.DataStreamId, Namespaces.StreamNamespace);

            await stream.SubscribeAsync((item, _) =>
                {
                    ReceivedEvent = true;
                    Console.WriteLine($"Next item received: '{item}'");
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

    public interface IDataProducer : IGrainWithStringKey
    {
        Task PostData(string text);
    }

    public class DataProducerGrain : Grain, IDataProducer
    {
        private IAsyncStream<string> Stream { get; set; }
        
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Namespaces.ProviderName);
            Stream = streamProvider.GetStream<string>(StreamIds.DataStreamId, Namespaces.StreamNamespace);
            
            return base.OnActivateAsync();
        }

        public async Task PostData(string text)
        {
            await Stream.OnNextAsync($"1-st part of {text}");
            //await Stream.OnNextAsync($"2-nd part of {text}");
            //await Stream.OnNextAsync($"3-rd part of {text}");

            //await Stream.OnErrorAsync(new Exception());
            //await Stream.OnCompletedAsync();
        }
    }
}