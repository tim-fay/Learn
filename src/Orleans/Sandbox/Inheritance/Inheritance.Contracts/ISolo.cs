using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface ISolo : IGrainWithStringKey
    {
        Task Speak();
    }

    public interface IHanSolo : ISolo
    {
    }

    public interface IFakeSolo : ISolo
    {
    }
}