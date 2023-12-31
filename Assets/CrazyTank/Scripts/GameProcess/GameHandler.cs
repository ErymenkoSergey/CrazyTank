using CrazyTank.Data;
using CrazyTank.Interface;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyTank.Core
{
    public sealed class GameHandler : MonoBehaviour, IInitializer, IControllable, IPaused
    {
        [Header("Data Configuration")]
        [SerializeField] private GameSetting _setting;
        [SerializeField] private WeaponsSetting _weapons;

        [Header("Interface Objects")]
        private GameObject _player;
        private IMoveble _iMoveble;
        private IShoot _iShoot;
        private IArmed _iArmed;

        [SerializeField] private GameObject _uI;
        private IDisplaying _iDisplaying;

        [SerializeField] private GameObject _spawner;
        private ISpawning _iSpawning;

        public event Action<bool> OnIsPause;

        public bool StartGame()
        {
            ClearPlayer();
            Initialized();
            _iSpawning.StartSpawn(_setting.MaxEnemyCount, this);
            return true;
        }

        private void ClearPlayer() => _player = null;

        public void Initialized()
        {
            if (_uI.TryGetComponent(out IDisplaying displaying))
            {
                _iDisplaying = displaying;
                _iDisplaying.SetControllable(this);
            }

            if (_spawner.TryGetComponent(out ISpawning spawning))
            {
                _iSpawning = spawning;
                _iSpawning.OnGameOver += GameOver;
            }
        }

        private void OnDestroy() => DeInitialized();

        public void DeInitialized() => _iSpawning.OnGameOver -= GameOver;

        public void SetPalyer(GameObject player)
        {
            _player = player;

            if (_player.TryGetComponent(out IMoveble iMoveble))
            {
                _iMoveble = iMoveble;
            }

            if (_player.TryGetComponent(out IShoot iShoot))
            {
                _iShoot = iShoot;
            }

            if (_player.TryGetComponent(out IArmed iArmed))
            {
                _iArmed = iArmed;
                _iArmed.SetWeaponsConfiguration(ref _weapons.Weapons, _iDisplaying);
            }
        }

        public IMoveble GetMoveble() => _iMoveble;
        public IShoot GetShoot() => _iShoot;

        public void SetPause(bool pause)
        {
            OnIsPause.Invoke(pause);
            Time.timeScale = pause ? 0 : 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void GameOver()
        {
            _iDisplaying.GameOver();
            SetPause(true);
        }

        public void RestartGame()
        {
            SetPause(false);
            SceneManager.LoadScene("Game");
        }
    }
}