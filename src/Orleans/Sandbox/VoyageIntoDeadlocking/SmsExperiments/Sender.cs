using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;
using VoyageIntoDeadlocking.ExplicitSubscriptionsGrains;

namespace VoyageIntoDeadlocking.SmsExperiments
{
    public interface ISender : IGrainWithStringKey
    {
        Task Broadcast(int value);
        Task TakeResponse(int value);
    }

    public interface IReceiver : IGrainWithGuidKey
    {
        
    }

    public class Message
    {
        public int Value { get; }
        public string Sender { get; }

        private Message(int value, string sender)
        {
            Value = value;
            Sender = sender;
        }

        public static Message New(int value, string sender)
        {
            return new Message(value, sender);
        }
    }
    
    public class Sender : Grain, ISender
    {
        public async Task Broadcast(int value)
        {
            var streamProvider = GetStreamProvider(ExplicitConstants.SmsStreamName);
            var stream = streamProvider.GetStream<Message>(Guid.Empty, ExplicitConstants.SmsStreamNamespace);
            Console.WriteLine($"Sending value: {value}.");
            await stream.OnNextAsync(Message.New(value, this.GetPrimaryKeyString()));
            Console.WriteLine($"Sent value: {value}.");
        }

        public Task TakeResponse(int value)
        {
            Console.WriteLine($"Response with new value: {value}.");
            return Task.CompletedTask;
        }
    }

    [ImplicitStreamSubscription(ExplicitConstants.SmsStreamNamespace)]
    public class Receiver : Grain, IReceiver
    {
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(ExplicitConstants.SmsStreamName);
            var stream = streamProvider.GetStream<Message>(Guid.Empty, ExplicitConstants.SmsStreamNamespace);
            await stream.SubscribeAsync(async (message, _) =>
            {
                Console.WriteLine($"Received value: {message.Value}.");
                var sender = GrainFactory.GetGrain<ISender>(message.Sender);
                int newValue = message.Value + 1;
                await sender.TakeResponse(newValue);
                Console.WriteLine($"Sent new value back: {newValue}.");
            });

            await base.OnActivateAsync();
        }
    }
}