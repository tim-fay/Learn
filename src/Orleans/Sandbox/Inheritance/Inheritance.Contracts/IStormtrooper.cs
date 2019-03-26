using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IStormtrooper<TWeapon> : IGrainWithStringKey
        where TWeapon : Weapon, new()
    {
        Task LoadUp();
        Task Fire();
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