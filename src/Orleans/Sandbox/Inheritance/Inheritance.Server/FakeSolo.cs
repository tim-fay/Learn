using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
    public class FakeSolo : Grain, IFakeSolo, ISolo
    {
        public Task Speak()
        {
            Console.WriteLine("I'm not real!!! Argh....");
            return Task.CompletedTask;
        }
    }
}