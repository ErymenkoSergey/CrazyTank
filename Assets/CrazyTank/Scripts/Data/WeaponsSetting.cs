using CrazyTank.Weapons;
using System;
using UnityEngine;

namespace CrazyTank.Data
{
    [CreateAssetMenu(fileName = "WeaponsSetting", menuName = "Game/Configuration/New WeaponsSetting")]
    public class WeaponsSetting : ScriptableObject
    {
        [SerializeField] private Weapon[] _weapons;
        public Weapon[] Weapons => _weapons;
    }

    [Serializable]
    public struct Weapon
    {
        public int Id;
        public string Name;
        public Texture2D Image;
        public float Speed;
        public float Damage;
        public GameObject GunPrefab;
        public Bullet BulletPrefab;
    }
}