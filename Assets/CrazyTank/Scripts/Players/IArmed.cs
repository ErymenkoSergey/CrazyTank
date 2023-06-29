using CrazyTank.Data;

namespace CrazyTank.Interface
{
    public interface IArmed
    {
        void SetWeaponsConfiguration(Weapon[] _weapons, IDisplaying displaying);
        void SetStatusGame(bool isPlay);
    }
}