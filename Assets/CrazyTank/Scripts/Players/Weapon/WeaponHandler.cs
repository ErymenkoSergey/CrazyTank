using CrazyTank.Data;
using CrazyTank.Input;
using CrazyTank.Interface;
using UnityEngine;

namespace CrazyTank.Weapons
{
    public sealed class WeaponHandler
    {
        public WeaponHandler(Weapon[] weapon, IDisplaying displaying)
        {
            Weapons = weapon;
            _ui = displaying;
        }

        private IDisplaying _ui;
        public Weapon[] Weapons { get; private set; }

        private int _currentWeapon = 0;

        private Weapon GetWeapon(int index) => Weapons[index];

        public int CurrentWeapon(int newIndex)
        {
            if (newIndex >= 0 && newIndex < Weapons.Length)
            {
                return _currentWeapon;
            }
            else
            {
                return _currentWeapon = 0;
            }
        }

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
            }

            Weapon gun = GetWeapon(CurrentWeapon(_currentWeapon));
            SetUI(gun.Image, gun.Name);
            return gun;
        }

        private void SetUI(Texture2D image, string name)
        {
            _ui.ChangeUIGun(image, name);
        }
    }
}