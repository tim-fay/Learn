using System;
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

            if (State.SubscriptionHandle != null)
            {
                await State.SubscriptionHandle.ResumeAsync(this);
            }
            
        }

        public async Task Init()
        {
            State.Data = 42;

            var streamProvider = GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<RecoverSignal>(RecoverChannelId.Value, nameof(RecoverChannelId));
            State.SubscriptionHandle = await stream.SubscribeAsync(this);

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
            State.Data = 0;
            await WriteStateAsync();
            await State.SubscriptionHandle.UnsubscribeAsync();
            State.SubscriptionHandle = null;
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