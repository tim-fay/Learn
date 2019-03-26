using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;

namespace Inheritance.Server
{
    public class Stormtrooper<TWeapon> : Grain, IStormtrooper<TWeapon>
        where TWeapon : Weapon, new()
    {
        private TWeapon Weapon { get; }
        
        public Stormtrooper()
        {
            Weapon = new TWeapon();
        }
        
        public Task LoadUp()
        {
            Console.WriteLine($"Trooper {this.GetPrimaryKeyString()} is loaded and ready!");
            return Task.CompletedTask;
        }

        public Task Fire()
        {
            Console.WriteLine($"Trooper {this.GetPrimaryKeyString()} fired with {Weapon.Name}!");
            return Task.CompletedTask;
        }
    }
}