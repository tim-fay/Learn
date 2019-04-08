using System.Threading.Tasks;
using Orleans;

namespace Inheritance.Contracts
{
    public interface IBarrack : IGrainWithStringKey
    {
        Task<IStormtrooper<TWeapon>> GetStormtrooper<TWeapon>(string callSign)
            where TWeapon : Weapon, new();

        Task SendStormtroopers(string starShipId);
    }
}