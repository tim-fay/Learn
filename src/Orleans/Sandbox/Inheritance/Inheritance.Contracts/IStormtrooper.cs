using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IStormtrooper : IGrainWithStringKey
    {
        Task LoadUp();
        Task Fire();
    }

    public interface IStormtrooper<TWeapon> : IStormtrooper, IGrainWithStringKey
        where TWeapon : Weapon, new()
    {
        Task CallForReinforcement<TRequestedWeapon>()
            where TRequestedWeapon : Weapon, new();
    }

    public abstract class Weapon
    {
        public string Name { get; protected set; }
    }

    public class Blaster : Weapon
    {
        public Blaster()
        {
            Name = "E-11";
        }
    }

    public class Pistol : Weapon
    {
        public Pistol()
        {
            Name = "DL-44";
        }
    }
}