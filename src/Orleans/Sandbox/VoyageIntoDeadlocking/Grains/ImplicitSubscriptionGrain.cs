using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.Grains
{
    public class Namespaces
    {
        public const string Subscription = "Stream";
        public const string Provider = "Provider";
        
    }
    
    interface IImplicitSubscription : IGrainWithStringKey
    {
        
    }
    
    [ImplicitStreamSubscription(Namespaces.Subscription)]
    public class ImplicitSubscriptionGrain : Grain, IImplicitSubscription
    {
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Namespaces.Provider);
            streamProvider.GetStream<string>()

            return base.OnActivateAsync();
        }
    }

    public interface IDataProducer
    {
        Task PostData(string text);
    }

    public class DataProducerGrain : Grain, IDataProducer
    {
        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider(Namespaces.Provider);
            streamProvider.GetStream<string>()
            
            return base.OnActivateAsync();
        }

        public Task PostData(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}