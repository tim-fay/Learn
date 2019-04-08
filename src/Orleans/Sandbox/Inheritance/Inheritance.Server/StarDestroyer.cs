using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;
using Orleans.Streams;

namespace Inheritance.Server
{
    public class StarDestroyer : Grain, IStarDestroyer
    {
        public async Task OpenGateway()
        {
            var streamProvider = GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<IStormtrooper>(ReinforcementChannels.StormtroopersBoardingChannel, nameof(ReinforcementChannels.StormtroopersBoardingChannel));
            var streamSubscriptionHandles = await stream.GetAllSubscriptionHandles();
            
            
            
            await stream.SubscribeAsync(async (trooper, _) =>
            {
                Console.WriteLine($"Trooper {trooper.GetPrimaryKeyString()} reached ship hangar.");
                await trooper.LoadUp();
                await trooper.Fire();
            });
        }
    }
}