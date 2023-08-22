using CrazyTank.Data;

namespace CrazyTank.Interface
{
    public interface IArmed
    {
        void SetWeaponsConfiguration(ref Weapon[] _weapons, IDisplaying displaying);
        void SetStatusGame(in bool isPlay);
    }
}