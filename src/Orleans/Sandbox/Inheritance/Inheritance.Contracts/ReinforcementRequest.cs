namespace Inheritance.Contracts
{
    public class ReinforcementRequest<TWeapon>
        where TWeapon : Weapon, new()
    {
        public string CallSignOfRequestingUnit { get; }
        public string WeaponName { get; }

        public ReinforcementRequest(string callSignOfRequestingUnit)
        {
            CallSignOfRequestingUnit = callSignOfRequestingUnit;
            WeaponName = new TWeapon().Name;
        }
    }
}