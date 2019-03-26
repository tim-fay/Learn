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
    }
}