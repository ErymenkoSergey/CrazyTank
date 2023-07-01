using CrazyTank.Input;

namespace CrazyTank.Interface
{
    public interface IShoot
    {
        void ChangeWeapon(WeaponDirection direction);
        void Fire(PressedStatus status);
    }
}