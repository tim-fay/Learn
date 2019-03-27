using System;
using System.Threading.Tasks;
using Inheritance.Contracts;
using Orleans;
using Orleans.Streams;

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

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("aqs");
            var streamId = GetReinforcementChannelByWeaponType<TWeapon>();
            var stream = streamProvider.GetStream<ReinforcementRequest<TWeapon>>(streamId, "reinforcement");
            var reinforcementChannel = stream.SubscribeAsync((reinforcement, token) =>
            {
                if (Weapon.Name == reinforcement.WeaponName)
                {
                    Console.WriteLine($"Stormtrooper {this.GetPrimaryKeyString()} is acting on request from {reinforcement.CallSignOfRequestingUnit} for weapon: {reinforcement.WeaponName}.");
                }

                return Task.CompletedTask;
            });

            return base.OnActivateAsync();
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

        public async Task CallForReinforcement<TRequestedWeapon>()
            where TRequestedWeapon : Weapon, new()
        {
            var streamProvider = GetStreamProvider("aqs");

            var streamId = GetReinforcementChannelByWeaponType<TRequestedWeapon>();
            var stream = streamProvider.GetStream<ReinforcementRequest<TRequestedWeapon>>(streamId, "reinforcement");
            var reinforcementRequest = new ReinforcementRequest<TRequestedWeapon>(this.GetPrimaryKeyString());
            await stream.OnNextAsync(reinforcementRequest);
        }

        private Guid GetReinforcementChannelByWeaponType<TReinforcementWeapon>()
            where TReinforcementWeapon : Weapon
        {
            if (typeof(TReinforcementWeapon) == typeof(Blaster))
            {
                return ReinforcementChannels.BlasterReinforcementChannel;
            }

            if (typeof(TReinforcementWeapon) == typeof(Pistol))
            {
                return ReinforcementChannels.PistolReinforcementChannel;
            }

            throw new InvalidOperationException("Unsupported weapon type");
        }
    }
}