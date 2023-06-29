using CrazyTank.Input;
using UnityEngine;

namespace CrazyTank.Interface
{
    public interface IMoveble
    {
        void Move(Vector3 direction, bool isOn);
        void Rotate(Vector3 direction, bool isOn);
        void ChangeWeapon(WeaponDirection direction);
        void Fire(PressedStatus status);
    }
}