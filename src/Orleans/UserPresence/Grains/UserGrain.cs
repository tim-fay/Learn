using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class UserGrain : Grain, IUser
    {
        public Task<bool> IsOnline()
        {
            throw new System.NotImplementedException();
        }
    }
}
