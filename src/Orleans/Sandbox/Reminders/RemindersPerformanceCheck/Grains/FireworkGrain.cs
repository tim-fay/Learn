using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace RemindersPerformanceCheck.Grains
{
    public interface IFireworkStarter : IGrainWithIntegerKey
    {
        Task StartTimer(TimeSpan timeToStartFirework);
    }
    
    public class FireworkGrain : Grain, IFireworkStarter, IRemindable
    {
        public async Task StartTimer(TimeSpan timeToStartFirework)
        {
            var reminder = await RegisterOrUpdateReminder(nameof(FireworkGrain), timeToStartFirework, TimeSpan.FromSeconds(60));
        }

        public async Task ReceiveReminder(string reminderName, TickStatus status)
        {
            var reminder = await GetReminder(reminderName);
            await UnregisterReminder(reminder);

            var fireworkCounter = GrainFactory.GetGrain<IFireworkCounter>(0);
            await fireworkCounter.IncrementByOne();
            
            Console.WriteLine($"{DateTime.Now}: Fire in the sky from: {this.GetPrimaryKeyLong().ToString()}");
        }
    }
}