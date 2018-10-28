using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
    public class Consumer : Grain, IConsumer
    {
        public async Task Consume(IUser user)
        {
            Console.WriteLine($"Consumption from {this.GetPrimaryKeyString()} for user: {user.GetPrimaryKeyLong()} begin");
            await user.DoSimple();
            Console.WriteLine($"Consumption from {this.GetPrimaryKeyString()} for user: {user.GetPrimaryKeyLong()} end");
        }
    }
}