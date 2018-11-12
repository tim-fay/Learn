using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.ExplicitSubscriptionsGrains
{
    public interface IRadioSource : IGrainWithStringKey
    {
        Task ReplyToSource(string replyMessage);
    }
}