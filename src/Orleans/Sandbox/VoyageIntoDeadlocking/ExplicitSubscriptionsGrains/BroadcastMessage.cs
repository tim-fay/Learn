namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public class BroadcastMessage
    {
        public string Message { get; }
        //public IRadioSource Source { get; }
        public string Source { get; }

        private BroadcastMessage(string message, string source)
        {
            Message = message;
            Source = source;
        }

        public static BroadcastMessage New(string message, string source)
        {
            return new BroadcastMessage(message, source);
        }

//        private BroadcastMessage(string message, IRadioSource source)
//        {
//            Message = message;
//            Source = source;
//        }
//
//        public static BroadcastMessage New(string message, IRadioSource source)
//        {
//            return new BroadcastMessage(message, source);
//        }
    }
}