using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
    public class SolodAndChewie : Grain, IHanSolo, IChewie
    {
        private readonly Guid _millenniumFalconId = Guid.NewGuid(); 
        
        public Task Speak()
        {
            Console.WriteLine($"{this.GetPrimaryKeyString()}: My name's Han Solo, from Millennium Falcon: {_millenniumFalconId}");
            return Task.CompletedTask;
        }

        public Task Roar()
        {
            Console.WriteLine($"{this.GetPrimaryKeyString()}: Aaaarghhh!!!!, from Millennium Falcon: {_millenniumFalconId}");
            return Task.CompletedTask;
        }
    }
}