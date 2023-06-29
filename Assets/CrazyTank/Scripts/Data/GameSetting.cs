using UnityEngine;

namespace CrazyTank.Data
{
    [CreateAssetMenu(fileName = "GameSetting", menuName = "Game/Configuration/New GameSetting")]
    public class GameSetting : ScriptableObject
    {
        [SerializeField] private int _maxEnemyCount = 10;
        public int MaxEnemyCount => _maxEnemyCount;
    }
}