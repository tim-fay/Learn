using System;
using System.Threading.Tasks;
using Stateless;
using Stateless.Graph;

namespace StatelessExperiments
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var userRealTimeStatus = new UserRealTimeStatus();
            userRealTimeStatus.OnActivateAsync();

            await userRealTimeStatus.Connected();
            Console.WriteLine(userRealTimeStatus.CurrentStatus);
            await userRealTimeStatus.Disconnected();
            Console.WriteLine(userRealTimeStatus.CurrentStatus);
        }
    }

    public class UserRealTimeStatus
    {
        public Status CurrentStatus { get; set; }
        
        private StateMachine<Status, Trigger> StateMachine { get; set; }

        public void OnActivateAsync()
        {
            CurrentStatus = Status.Default;
            StateMachine = new StateMachine<Status, Trigger>(
                () => CurrentStatus, 
                state => CurrentStatus = state);
            ConfigureStateMachineInstance(StateMachine, (transition) =>
            {
                Console.WriteLine($"state transitioned to {transition.Destination} by {transition.Trigger}");
                return Task.CompletedTask;
            });
        }

        public async Task<bool> Connected()
        {
            return await TryTransition(Trigger.Connected);
        }
//
//        public async Task<bool> Ringing()
//        {
//            return await TryTransition(Trigger.ReceivedCall);
//        }
//
//        public async Task<bool> Calling()
//        {
//            return await TryTransition(Trigger.CallStarted);
//        }
//
//        public async Task<bool> Answering()
//        {
//            return await TryTransition(Trigger.AnsweredCall);
//        }
//
//        public async Task<bool> Declining()
//        {
//            return await TryTransition(Trigger.HangedUp);
//        }
//
//        public async Task<bool> LeavingConference()
//        {
//            return await TryTransition(Trigger.HangedUp);
//        }
//
//        public async Task<bool> CancelCalling()
//        {
//            return await TryTransition(Trigger.HangedUp);
//        }
//
//        public async Task<bool> InConference()
//        {
//            return await TryTransition(Trigger.StartedConference);
//        }
//
//        public async Task<bool> HangUp()
//        {
//            return await TryTransition(Trigger.HangedUp);
//        }
//
        public async Task<bool> Disconnected()
        {
            return await TryTransition(Trigger.Disconnected);
        }

        private static void ConfigureStateMachineInstance(StateMachine<Status, Trigger> stateMachine, Func<StateMachine<Status, Trigger>.Transition, Task> onStateTransitionAction)
        {
            stateMachine.Configure(Status.Offline)
                .Permit(Trigger.Connected, Status.Online);

            stateMachine.Configure(Status.Online)
                .Permit(Trigger.Disconnected, Status.Offline)
                .Permit(Trigger.ReceivedCall, Status.Ringing)
                .Permit(Trigger.CallStarted, Status.ReadyForConference)
                .Permit(Trigger.SwitchedToAway, Status.Away)
                .Permit(Trigger.SwitchedToOnCall, Status.OnCall)
                .Permit(Trigger.SwitchedToTelephone, Status.Telephone);

            stateMachine.Configure(Status.Away)
                .Permit(Trigger.Disconnected, Status.Offline)
                .Permit(Trigger.SwitchedToOnline, Status.Online);

            stateMachine.Configure(Status.OnCall)
                .Permit(Trigger.Disconnected, Status.Offline)
                .Permit(Trigger.SwitchedToOnline, Status.Online);

            stateMachine.Configure(Status.Telephone)
                .Permit(Trigger.Disconnected, Status.Offline)
                .Permit(Trigger.SwitchedToOnline, Status.Online);

            stateMachine.Configure(Status.Ringing)
                .Permit(Trigger.Disconnected, Status.Offline)
                .Permit(Trigger.AnsweredCall, Status.ReadyForConference)
                .Permit(Trigger.HangedUp, Status.Online);

            stateMachine.Configure(Status.ReadyForConference)
                .Permit(Trigger.Disconnected, Status.Offline)
                .Permit(Trigger.StartedConnectingToConference, Status.ConnectingToConference)
                .Permit(Trigger.HangedUp, Status.Online);

            stateMachine.Configure(Status.ConnectingToConference)
                .Permit(Trigger.StartedConference, Status.InConference)
                .Permit(Trigger.HangedUp, Status.Online);

            stateMachine.Configure(Status.InConference)
                .Permit(Trigger.Disconnected, Status.DisconnectedButStillInConference)
                .Permit(Trigger.HangedUp, Status.Online);

            stateMachine.Configure(Status.DisconnectedButStillInConference)
                .Permit(Trigger.Connected, Status.InConference)
                .Permit(Trigger.HangedUp, Status.Offline);

            stateMachine.OnTransitionedAsync(_ => onStateTransitionAction(_));
        }

        private async Task<bool> TryTransition(Trigger trigger)
        {
            if (!StateMachine.CanFire(trigger))
            {
                return false;
            }

            await StateMachine.FireAsync(trigger);
            return true;
        }
    }
    
    public enum Trigger
    {
        Connected,
        Disconnected,
        CallStarted,
        ReceivedCall,
        AnsweredCall,
        HangedUp,
        StartedConnectingToConference,
        StartedConference,
        SwitchedToAway,
        SwitchedToTelephone,
        SwitchedToOnCall,
        SwitchedToOnline,
    }

        public enum Status
    {
        /// <summary>
        /// Default status. Offline.
        /// </summary>
        Default = Offline,

        /// <summary>
        /// User is not available for any activities regarding his role.
        /// </summary>
        Offline = 0,

        /// <summary>
        /// User is available for any activities regarding his role.
        /// Users can change their status manually in order to set this status.
        /// </summary>
        Online = 1,

        /// <summary>
        /// User is not available for any activities regarding his role.
        /// Users can change their status manually in order to set this status.
        /// </summary>
        Away = 2,

        /// <summary>
        /// User has incoming call. This status can ba set only programmatically.
        /// </summary>
        Ringing = 3,

        /// <summary>
        /// User has this status when he start call or when he answer call.
        /// This status can ba set only programmatically.
        /// </summary>
        ReadyForConference = 4,

        /// <summary>
        /// User ready for communication, but he waiting provider connection information to connect to the Conference.
        /// This status can ba set only programmatically.
        /// </summary>
        ConnectingToConference = 5,

        /// <summary>
        /// InConference. User is in a conference.
        /// This status can ba set only programmatically.
        /// </summary>
        InConference = 6,

        /// <summary>
        /// Telephone. When user can receive only real phone calls.
        /// User is not available for activities in the system.
        /// Users can change their status manually in order to set this status.
        /// </summary>
        Telephone = 7,

        /// <summary>
        /// OnCall. When user can receive only sms about important calls.
        /// User is not available for activities in the system.
        /// Users can change their status manually in order to set this status.
        /// </summary>
        OnCall = 8,

        /// <summary>
        /// DisconnectedButStillInConference. When user client disconnected from server,
        /// but he still in conference.
        /// </summary>
        DisconnectedButStillInConference = 9
    }

}