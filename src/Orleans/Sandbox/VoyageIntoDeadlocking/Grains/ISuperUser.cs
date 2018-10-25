using System;
using System.Threading.Tasks;
using Orleans;

namespace VoyageIntoDeadlocking.Grains
{
    public interface ISuperUser : IUser, IGrainWithIntegerKey
    {
        Task DoSuper();
    }

    public class SuperUser : Grain, ISuperUser
    {
        public Task DoSimple()
        {
            Console.WriteLine($"Simple from {this.GetPrimaryKeyLong()}");
            return Task.CompletedTask;
        }

        public Task DoSuper()
        {
            Console.WriteLine($"Super from {this.GetPrimaryKeyLong()}");
            return Task.CompletedTask;
        }
    }

    public interface IUser : IGrainWithIntegerKey
    {
        Task DoSimple();
    }

    public interface IConsumer : IGrainWithStringKey
    {
        Task Consume(IUser user);
    }

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