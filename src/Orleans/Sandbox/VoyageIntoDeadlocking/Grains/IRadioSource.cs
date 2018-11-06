using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.Grains
{
    public interface IRadioSource : IGrainWithStringKey
    {
        Task ReplyToSource(string replyMessage);
    }
}