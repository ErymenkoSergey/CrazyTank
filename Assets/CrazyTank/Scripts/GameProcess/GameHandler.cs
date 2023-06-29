using CrazyTank.Data;
using CrazyTank.Interface;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrazyTank.Core
{
    public class GameHandler : MonoBehaviour, IInitializer, IControllable, IPaused
    {
        [Header("Data Configuration")]
        [SerializeField] private GameSetting _setting;
        [SerializeField] private WeaponsSetting _weapons;

        [Header("Interface Objects")]
        private GameObject _player;
        private IMoveble _iMoveble;
        private IArmed _iArmed;

        [SerializeField] private GameObject _uI;
        private IDisplaying _iDisplaying;

        [SerializeField] private GameObject _spawner;
        private ISpawning _iSpawning;

        public event Action<bool> OnPause;

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

        public void DeInitialized()
        {
            _iSpawning.OnGameOver -= GameOver;
        }

        public void SetPalyer(GameObject player)
        {
            _player = player;

            if (_player.TryGetComponent(out IMoveble iMoveble))
            {
                _iMoveble = iMoveble;
            }

            if (_player.TryGetComponent(out IArmed armed))
            {
                _iArmed = armed;
                _iArmed.SetWeaponsConfiguration(_weapons.Weapons, _iDisplaying);
            }
        }

        public IMoveble GetMoveble() => _iMoveble;

        public void SetPause(bool pause)
        {
            OnPause.Invoke(pause);
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