using System.Numerics;
using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IFibonacciNumber : IGrainWithIntegerKey
    {
        Task<BigInteger> CalculateNumber();
    }
}