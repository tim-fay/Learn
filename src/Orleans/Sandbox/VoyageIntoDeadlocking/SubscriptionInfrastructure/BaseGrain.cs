using System;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.SubscriptionInfrastructure
{
    public static class StreamSubscriptionExtensions
    {
        public static async Task SubscribeOrResume<T>(this IStreamProvider streamProvider, Guid streamId, string streamNamespace, Func<T, Task> onNext)
        {
            var stream = streamProvider.GetStream<T>(streamId, streamNamespace);
            
            var subscriptions = await stream.GetAllSubscriptionHandles();
            if (subscriptions.Count == 0)
            {
                // If no subscription exist, make a new subscription
                await stream.SubscribeAsync(async (message, _) => await onNext(message));
            }
            else
            {
                // Should be only one existing subscription, as this base API does not support multiple subscriptions 
                await subscriptions.Single().ResumeAsync(async (message, _) => await onNext(message));
            }
        }

        public static async Task Unsubscribe<T>(this IStreamProvider streamProvider, Guid streamId, string streamNamespace)
        {
            var stream = streamProvider.GetStream<T>(streamId, streamNamespace);
            var subscriptions = await stream.GetAllSubscriptionHandles();
            var streamSubscription = subscriptions.SingleOrDefault();
            // We're making un-subscription logic idempotent, so several un-subscription calls can be made safely
            if (streamSubscription != null)
            {
                // Only one real subscription will be cancelled 
                await streamSubscription.UnsubscribeAsync();
            }
        }
    }
}