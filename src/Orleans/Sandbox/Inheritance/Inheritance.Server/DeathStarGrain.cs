using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
    public class DeathStarCore
    {
        private long _dummy1 = 666;
        private int _energyLevel = 0;
        private long _dummy2 = 777;

        public void SetCoreEnergyLevel(int level)
        {
            _energyLevel = level;
        }

        public int GetLevel()
        {
            return _energyLevel;
        }
    }
    
    public class DeathStarGrain : Grain<DeathStarCore>, IDeathStar
    {
        public override Task OnActivateAsync()
        {
            var level = State.GetLevel();
            return base.OnActivateAsync();
        }

        public async Task Build()
        {
            State.SetCoreEnergyLevel(100);
            await WriteStateAsync();
        }

        public async Task Explode()
        {
            State.SetCoreEnergyLevel(int.MaxValue);
            await WriteStateAsync();
        }
    }
}