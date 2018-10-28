using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
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
}