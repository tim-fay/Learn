using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
    public class StormtrooperBarrack: Grain, IBarrack
    {
        public async Task<IStormtrooper<TWeapon>> GetStormtrooper<TWeapon>(string callSign)
            where TWeapon : Weapon, new()
        {
            var stormtrooper = GrainFactory.GetGrain<IStormtrooper<TWeapon>>(callSign);
            await stormtrooper.LoadUp();
            return stormtrooper;
        }

        public async Task SendStormtroopers(string starShipId)
        {
            var streamProvider = GetStreamProvider("aqs");
            var stream = streamProvider.GetStream<IStormtrooper>(ReinforcementChannels.StormtroopersBoardingChannel, nameof(ReinforcementChannels.StormtroopersBoardingChannel));

            var stormtrooper1 = GrainFactory.GetGrain<IStormtrooper<Blaster>>("FN-555");
            var stormtrooper2 = GrainFactory.GetGrain<IStormtrooper<Blaster>>("FN-556");
            var stormtrooper3 = GrainFactory.GetGrain<IStormtrooper<Pistol>>("FN-655");
            var stormtrooper4 = GrainFactory.GetGrain<IStormtrooper<Pistol>>("FN-656");

            await stream.OnNextAsync(stormtrooper1);
            await stream.OnNextAsync(stormtrooper2);
            await stream.OnNextAsync(stormtrooper3);
            await stream.OnNextAsync(stormtrooper4);
        }
    }
}