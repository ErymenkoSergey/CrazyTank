using CrazyTank.Interface;
using UnityEngine;
using UnityEngine.InputSystem;
using InputControl = CrazyTank.Input.InputControl;

namespace CrazyTank.Core
{
    public sealed class AppStartup : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        private InputControl _input;

        [SerializeField] private GameObject _handler;
        private IControllable _game;
        private IPaused _paused;

        [SerializeField] private bool _isStartGame = false;

        private void Start()
        {
            CreateReferences();
            Initialized();
        }

        private void CreateReferences()
        {
            _input = new InputControl(_inputActions);

            if (_handler.TryGetComponent(out IControllable handler))
            {
                _game = handler;
            }
            if (_handler.TryGetComponent(out IPaused paused))
            {
                _paused = paused;
            }
        }

        private void Initialized()
        {
            _isStartGame = _game.StartGame();
            _input.SetPlayer(_game.GetMoveble(), _game.GetShoot());
            _input.SetPaused(_paused);
        }

        private void OnDestroy()
        {
            _input.DeInitialized();
            _input = null;
        }
    }
}