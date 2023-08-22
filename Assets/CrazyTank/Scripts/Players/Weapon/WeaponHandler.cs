using CrazyTank.Data;
using CrazyTank.Input;
using CrazyTank.Interface;
using UnityEngine;

namespace CrazyTank.Weapons
{
    public sealed class WeaponHandler
    {
        public WeaponHandler(ref Weapon[] weapon, IDisplaying displaying)
        {
            Weapons = weapon;
            _ui = displaying;
        }

        private IDisplaying _ui;
        private Weapon[] Weapons;
        private int _currentWeapon = 0;

        public Weapon GetWeapon(WeaponDirection selected)
        {
            switch (selected)
            {
                case WeaponDirection.None:
                    Debug.LogError($"Not found direction ");
                    break;
                case WeaponDirection.Up:
                    _currentWeapon++;
                    break;
                case WeaponDirection.Down:
                    _currentWeapon--;
                    break;
                default:
                    Debug.LogError($"Not found direction ");
                    break;
            }

            Weapon gun = GetWeapon(ref CurrentWeapon(ref _currentWeapon));
            SetUI(ref gun.Image, ref gun.Name);
            return gun;
        }

        private Weapon GetWeapon(ref int index) => Weapons[index];

        public ref int CurrentWeapon(ref int newIndex)
        {
            if (newIndex >= 0 && newIndex < Weapons.Length)
            {
                return ref _currentWeapon;
            }
            else
            {
                _currentWeapon = 0;
                return ref _currentWeapon;
            }
        }

        private void SetUI(ref Texture2D image, ref string name) => _ui.ChangeUIGun(ref image, ref name);
    }
}