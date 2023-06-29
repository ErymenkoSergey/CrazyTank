using CrazyTank.Characters;
using System;
using UnityEngine;

namespace CrazyTank.Data
{
    [CreateAssetMenu(fileName = "CharactersSetting", menuName = "Game/Configuration/New CharactersSetting")]
    public class CharactersSetting : ScriptableObject
    {
        [SerializeField] private Character _player;
        public Character Player => _player;

        [SerializeField] private Character[] _enemy;
        public Character[] Enemy => _enemy;
    }

    [Serializable]
    public struct Character
    {
        public PlayerConfiguration Configuration;
        public BaseCharacter Prefab;
    }

    public enum CharacterType
    {
        None = 0,
        Player = 1,
        Enemy = 2
    }

    [Serializable]
    public struct PlayerConfiguration
    {
        public float Health;
        [Range(0.01f, 1)] public float Armor;
        public float MoveSpeed;
        public float Damage;
    }
}