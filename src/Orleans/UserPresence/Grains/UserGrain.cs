using System;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    public class UserGrain : Grain, IUser
    {
        private DateTime LastHeartbeatMessageTime { get; set; }

        public Task Heartbeat()
        {
            LastHeartbeatMessageTime = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task<bool> IsOnline()
        {
            return Task.FromResult(IsAlive());
        }

        private bool IsAlive()
        {
            return DateTime.UtcNow - LastHeartbeatMessageTime < Defaults.HeartbeatDelay;
        }
    }

    public static class Defaults
    {
        public static readonly TimeSpan HeartbeatDelay = TimeSpan.FromSeconds(5);
    }
}
