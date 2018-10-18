using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.Grains
{
    public class BroadcastMessage
    {
        public string Message { get; }
        public IRadioSource Source { get; }

        private BroadcastMessage(string message, IRadioSource source)
        {
            Message = message;
            Source = source;
        }

        public static BroadcastMessage New(string message, IRadioSource source)
        {
            return new BroadcastMessage(message, source);
        }
    }

    public interface IRadioControl : IGrainWithGuidKey
    {
        Task BroadcastMessage(string message);
    }

    public interface IRadioSource : IGrainWithGuidKey
    {
        Task Listen(string replyMessage);
    }
    
    public class RadioSourceGrain : Grain, IRadioControl, IRadioSource
    {
        private IAsyncStream<BroadcastMessage> RadioStream { get; set; }
        
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Streams.RadioStreamName);
            RadioStream = streamProvider.GetStream<BroadcastMessage>(Streams.RadioStreamId, Streams.RadioStreamName);
            return base.OnActivateAsync();
        }

        public async Task BroadcastMessage(string message)
        {
            await RadioStream.OnNextAsync(Grains.BroadcastMessage.New("Message into the void!", this));
        }

        public Task Listen(string replyMessage)
        {
            Console.WriteLine($"Received reply message: '{replyMessage}' from outer space.");
            return Task.CompletedTask;
        }
    }

    public interface IAlienPlanet : IGrainWithGuidKey
    {
        Task Create();
    }

    public class AlienPlanetGrain : Grain, IAlienPlanet
    {
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Streams.RadioStreamName);
            var stream = streamProvider.GetStream<BroadcastMessage>(Streams.RadioStreamId, Streams.RadioStreamName);
            stream.SubscribeAsync(async (message, _) =>
            {
                var source = message.Source;
                Console.WriteLine($"Message received: '{message.Message}' from {source.GetPrimaryKey().ToString()}");
                //await source.Listen($"Hello out there from planet: {this.GetPrimaryKey().ToString()}!");
            });
            
            return base.OnActivateAsync();
        }

        public Task Create()
        {
            Console.WriteLine($"Planet {this.GetPrimaryKey().ToString()} created.");
            return Task.CompletedTask;
        }
    }
}