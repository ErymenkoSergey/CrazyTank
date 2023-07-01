using System;
using UnityEngine;

namespace CrazyTank.Interface
{
    public interface ISpawning
    {
        event Action<Vector3> OnGetPosition;
        event Action OnGameOver;
        void StartSpawn(int countEnemy, IControllable controllable);
        void SetPalyer(GameObject player);
        void SetTarget(Vector3 vector);
        void Respawn(int key);
        void GameOver();
    }
}