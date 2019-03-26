namespace VoyageIntoDeadlocking.ImplicitSubscriptionsGrains
{
    public enum WhatType
    {
        Ringing,
        Connecting,
        Whatever,
    }
    
    public interface IEvent
    {
        WhatType What { get; }
    }

    public class Ringing : IEvent
    {
        public WhatType What { get; } = WhatType.Ringing;
    }

    public class Connecting : IEvent
    {
        public WhatType What { get; } = WhatType.Connecting;
    }

    public class Whatever : IEvent
    {
        public WhatType What { get; } = WhatType.Whatever;
    }
}