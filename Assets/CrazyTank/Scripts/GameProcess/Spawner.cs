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
        private Dictionary<int, BaseCharacter> _enemyPool = new Dictionary<int, BaseCharacter>();

        private int _keyIndexRespawn;

        [SerializeField] private int _spawnEnemyCountToDie = 1;

        public event Action OnGameOver;
        public event Action<Vector3> OnGetPosition;

        private bool _isFirstSpawn = true;

        public void StartSpawn(int countEnemy, IControllable controllable)
        {
            ClearCharacter();
            _iControllable = controllable;
            PlayerSpawn();
            EnemySpawn(countEnemy);
        }

        private void ClearCharacter() => _enemyPool.Clear();

        public void PlayerSpawn()
        {
            Character player = _data.Player;
            CreateCharacter(0,ref player);
        }

        public void EnemySpawn(int countEnemy)
        {
            for (int i = 0; i < countEnemy; i++)
            {
                int random = GetRandomEnemy();
                CreateCharacter(GetSpawnIndex(), ref _data.Enemy[random], _isFirstSpawn ? i : _keyIndexRespawn);
                _enemyPool.Add(_isFirstSpawn ? i : _keyIndexRespawn, _data.Enemy[random].Prefab);
            }

            _isFirstSpawn = false;
        }

        private void CreateCharacter(int indexSpawn, ref Character character, int id = 0)
        {
            BaseCharacter player = Instantiate(character.Prefab, _spawnPoints[indexSpawn]);
            player.SetData(ref character.Configuration, ref id, this);
        }

        private int GetRandomEnemy() => UnityEngine.Random.Range(0, _data.Enemy.Length);
        private int GetSpawnIndex() => UnityEngine.Random.Range(1, _spawnPoints.Length);

        public void Respawn(ref int key)
        {
            _enemyPool.Remove(key);
            _keyIndexRespawn = key;
            EnemySpawn(_spawnEnemyCountToDie);
        }

        public void GameOver() => OnGameOver.Invoke();
        public void SetTarget(Vector3 vector) => OnGetPosition?.Invoke(vector);
        public void SetPalyer(GameObject player) => _iControllable.SetPalyer(player);
    }
}