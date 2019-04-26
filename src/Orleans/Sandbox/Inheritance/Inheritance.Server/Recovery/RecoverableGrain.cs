using System;
using System.Linq;
using System.Threading.Tasks;
using Inheritance.Contracts.Recovery;
using Orleans;
using Orleans.Streams;

namespace Inheritance.Server.Recovery
{
    public class RecoverableGrain : Grain<RecoverableState>, IRecoverableGrain, IAsyncObserver<RecoverSignal>
    {
        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            var streamProvider = GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<RecoverSignal>(RecoverChannelId.Value, nameof(RecoverChannelId));
            var subscriptions = await stream.GetAllSubscriptionHandles();
            var subscriptionHandle = subscriptions.SingleOrDefault();

            if (subscriptionHandle != null)
            {
                await subscriptionHandle.ResumeAsync(this);
            }
            
        }

        public async Task Init()
        {
            State.Data = 42;

            var streamProvider = GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<RecoverSignal>(RecoverChannelId.Value, nameof(RecoverChannelId));
            var subscriptions = await stream.GetAllSubscriptionHandles();
            var subscriptionHandle = subscriptions.SingleOrDefault();

            if (subscriptionHandle != null)
            {
                await subscriptionHandle.ResumeAsync(this);
            }
            else
            {
                await stream.SubscribeAsync(this);
            }

            await WriteStateAsync();
            Console.WriteLine($"Init completed for {this.GetPrimaryKeyLong().ToString()}");
        }

        public Task DeactivateExplicit()
        {
            DeactivateOnIdle();
            return Task.CompletedTask;
        }

        public Task<int> ReadState()
        {
            return Task.FromResult(State.Data);
        }

        async Task IAsyncObserver<RecoverSignal>.OnNextAsync(RecoverSignal item, StreamSequenceToken token)
        {
            var streamProvider = GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<RecoverSignal>(RecoverChannelId.Value, nameof(RecoverChannelId));
            var subscriptions = await stream.GetAllSubscriptionHandles();
            var subscriptionHandle = subscriptions.Single();
            await subscriptionHandle.UnsubscribeAsync();

            State.Data = 0;
            await WriteStateAsync();
        }

        Task IAsyncObserver<RecoverSignal>.OnCompletedAsync()
        {
            Console.WriteLine("OnCompletedAsync called.");
            return Task.CompletedTask;
        }

        Task IAsyncObserver<RecoverSignal>.OnErrorAsync(Exception ex)
        {
            Console.WriteLine("OnErrorAsync called.");
            return Task.CompletedTask;
        }
    }
}