using System;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public static class ExplicitConstants
    {
        public const string SmsStreamName = "SMS"; 
        public const string SmsStreamNamespace = "SmsNamespace"; 
        public const string RadioStreamName = "Voyager"; 
        public const string RadioStreamNamespace = "VoyagerNamespace"; 
        public static readonly Guid RadioStreamId = Guid.Empty; 
    }
}