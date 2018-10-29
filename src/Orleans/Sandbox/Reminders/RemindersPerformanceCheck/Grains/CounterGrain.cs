using System.Threading.Tasks;
using Orleans;

namespace RemindersPerformanceCheck.Grains
{
    public interface IFireworkCounter : IGrainWithIntegerKey
    {
        Task IncrementByOne();
        Task<long> ReadCurrentValue();
    }
    
    public class FireworkCounterGrain : Grain, IFireworkCounter
    {
        private long Counter { get; set; }
        
        public Task IncrementByOne()
        {
            Counter++;
            return Task.CompletedTask;
        }

        public Task<long> ReadCurrentValue()
        {
            return Task.FromResult(Counter);
        }
    }
}