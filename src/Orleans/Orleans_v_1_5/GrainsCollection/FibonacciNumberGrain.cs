using System.Numerics;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;
using Orleans.Runtime;

namespace GrainsCollection
{
    //public class FibonacciNumberGrain : Grain<BigInteger?>, IFibonacciNumber
    public class FibonacciNumberGrain : Grain, IFibonacciNumber
    {
        private BigInteger? _state;

        public async Task<BigInteger> CalculateNumber()
        {
            if (!_state.HasValue)
            {
                _state = await DoCalculationsAsync();
            }

            return _state.Value;
        }

        private async Task<BigInteger> DoCalculationsAsync()
        {
            long calculatedNumberIndex = this.GetPrimaryKeyLong();
            GetLogger().Info($"Doing calculations for {calculatedNumberIndex}");

            // Check for basic indexes
            if (calculatedNumberIndex == 1 || calculatedNumberIndex == 2)
            {
                GetLogger().Info("Index equals 1|2, skipping main calculations");
                return BigInteger.One;
            }

            var number1 = GrainFactory.GetGrain<IFibonacciNumber>(calculatedNumberIndex - 2).CalculateNumber();
            var number2 = GrainFactory.GetGrain<IFibonacciNumber>(calculatedNumberIndex - 1).CalculateNumber();

            await Task.WhenAll(number1, number2);

            return BigInteger.Add(number1.Result, number2.Result);
        }

    }
}