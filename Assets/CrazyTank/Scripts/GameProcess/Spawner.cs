using CrazyTank.Characters;
using CrazyTank.Data;
using CrazyTank.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyTank.Core
{
    public sealed class Spawner : MonoBehaviour, ISpawning
    {
        [SerializeField] private CharactersSetting _data;
        private IControllable _iControllable;

        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Dictionary<int, BaseCharacter> _enemyPool = new Dictionary<int, BaseCharacter>();
        private int _keyIndexRespawn;

        [SerializeField] private int _spawnEnemyCountToDie = 1;

        public event Action OnGameOver;
        public event Action<Vector3> OnGetPosition;

        public void StartSpawn(int countEnemy, IControllable controllable)
        {
            ClearCharacter();
            _iControllable = controllable;
            PlayerSpawn();
            EnemySpawn(countEnemy, true);
        }

        private void ClearCharacter() => _enemyPool.Clear();

        public GameObject PlayerSpawn()
        {
            var player = _data.Player;
            var players = CreateCharacter(0, player);

            return players.gameObject;
        }

        public void EnemySpawn(int countEnemy, bool firstSpawn)
        {
            if (firstSpawn)
            {
                for (int i = 0; i < countEnemy; i++)
                {
                    int random = UnityEngine.Random.Range(0, _data.Enemy.Length);
                    CreateCharacter(GetSpawnIndex(), _data.Enemy[random], i);
                    _enemyPool.Add(i, _data.Enemy[random].Prefab);
                }
            }
            else
            {
                for (int i = 0; i < countEnemy; i++)
                {
                    int random = UnityEngine.Random.Range(0, _data.Enemy.Length);
                    CreateCharacter(GetSpawnIndex(), _data.Enemy[random], _keyIndexRespawn);
                    _enemyPool.Add(_keyIndexRespawn, _data.Enemy[random].Prefab);
                }
            }
        }

        private BaseCharacter CreateCharacter(int indexSpawn, Character character, int id = 0)
        {
            BaseCharacter player = Instantiate(character.Prefab, _spawnPoints[indexSpawn]);
            player.SetData(character.Configuration, id, this);
            return player;
        }

        private int GetSpawnIndex() => UnityEngine.Random.Range(1, _spawnPoints.Length);

        public void ReSpawn(int key)
        {
            _enemyPool.Remove(key);
            _keyIndexRespawn = key;
            EnemySpawn(_spawnEnemyCountToDie, false);
        }

        public void GameOver() => OnGameOver?.Invoke();

        public void SetTarget(Vector3 vector) => OnGetPosition?.Invoke(vector);

        public void SetPalyer(GameObject player) => _iControllable.SetPalyer(player);
    }
}