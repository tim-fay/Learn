using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Streams;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public interface ISecondStreamGrain : IGrainWithStringKey
    {
        Task SubscribeToStream();
    }
    
    public class SecondStreamGrain : Grain, ISecondStreamGrain
    {
        public async Task SubscribeToStream()
        {
            var streamProvider = GetStreamProvider(ExplicitConstants.RadioStreamName);
            var stream = streamProvider.GetStream<string>(ExplicitConstants.RadioStreamId, ExplicitConstants.RadioStreamNamespace);
            await stream.SubscribeAsync((s, token) =>
            {
                Console.WriteLine($"Value: {s}");
                return Task.CompletedTask;
            });
        }
    }
}