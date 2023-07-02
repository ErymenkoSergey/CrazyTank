using CrazyTank.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrazyTank.Input
{
    public enum PressedStatus
    {
        None = 0,
        Down = 1,
        Pressed = 2,
        Up = 3
    }

    public enum WeaponDirection
    {
        None = 0,
        Up = 1,
        Down = 2
    }

    public sealed class InputControl 
    {
        public InputControl(InputActionAsset inputs)
        {
            _inputActions = inputs;
        }

        private IMoveble _iMoveblePlayer;
        private IShoot _iShootPlayer;
        private IPaused _iPaused;
        private bool _isPause;

        private InputActionAsset _inputActions;
        private InputActionMap _playerActionMap;

        private string _nameMap = "Player";

        private string _forwardCommand = "ForwardMove";
        private string _leftCommand = "LeftMove";
        private string _rightCommand = "RightMove";
        private string _backCommand = "BackMove";

        private string _fireCommand = "Fire";

        private string _changeWeaponUpCommand = "ChangeWeaponUp";
        private string _changeWeaponDownCommand = "ChangeWeaponDown";

        private InputAction _forward;
        private InputAction _left;
        private InputAction _right;
        private InputAction _back;

        private InputAction _fire;

        private InputAction _changeWeaponUp;
        private InputAction _changeWeaponDown;

        public void Initialized()
        {
            SetLinks();
            Subscribe();
        }

        public void DeInitialized()
        {
            _iPaused.OnIsPause -= SetStatusPause;
            UnSubscribe();
        }

        private void SetLinks()
        {
            _playerActionMap = _inputActions.FindActionMap(_nameMap);

            _forward = _playerActionMap.FindAction(_forwardCommand);
            _left = _playerActionMap.FindAction(_leftCommand);
            _right = _playerActionMap.FindAction(_rightCommand);
            _back = _playerActionMap.FindAction(_backCommand);

            _fire = _playerActionMap.FindAction(_fireCommand);

            _changeWeaponUp = _playerActionMap.FindAction(_changeWeaponUpCommand);
            _changeWeaponDown = _playerActionMap.FindAction(_changeWeaponDownCommand);
        }

        private void Subscribe()
        {
            _forward.started += ForwardMove;
            _forward.canceled += ForwardMove;

            _left.started += LeftMove;
            _left.canceled += LeftMove;

            _right.started += RightMove;
            _right.canceled += RightMove;

            _back.started += BackMove;
            _back.canceled += BackMove;

            _fire.started += Fire;

            _changeWeaponUp.started += ChangeWeaponUp;

            _changeWeaponDown.started += ChangeWeaponDown;

            _playerActionMap.Enable();
            _inputActions.Enable();
        }

        private void UnSubscribe()
        {
            _forward.started -= ForwardMove;
            _forward.canceled -= ForwardMove;

            _left.started -= LeftMove;
            _left.canceled -= LeftMove;
            _right.started -= RightMove;
            _right.canceled -= RightMove;
            _back.started -= BackMove;
            _back.canceled -= BackMove;

            _fire.started -= Fire;

            _changeWeaponUp.started -= ChangeWeaponUp;

            _changeWeaponDown.started -= ChangeWeaponDown;

            _playerActionMap.Disable();
            _inputActions.Disable();
        }

        public void SetPlayer(IMoveble player, IShoot shoot)
        {
            Initialized();

            _iMoveblePlayer = player;
            _iShootPlayer = shoot;
        }

        private void ForwardMove(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iMoveblePlayer.Move(Vector3.forward, true);
            if (Context.canceled)
                _iMoveblePlayer.Move(Vector3.forward, false);
        }

        private void BackMove(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iMoveblePlayer.Move(Vector3.back, true);
            if (Context.canceled)
                _iMoveblePlayer.Move(Vector3.back, false);
        }

        private void LeftMove(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iMoveblePlayer.Rotate(Vector3.left, true);
            if (Context.canceled)
                _iMoveblePlayer.Rotate(Vector3.left, false);
        }

        private void RightMove(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iMoveblePlayer.Rotate(Vector3.right, true);
            if (Context.canceled)
                _iMoveblePlayer.Rotate(Vector3.right, false);
        }

        private void Fire(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iShootPlayer.Fire(PressedStatus.Down);
        }

        private void ChangeWeaponUp(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iShootPlayer.ChangeWeapon(WeaponDirection.Up);
        }

        private void ChangeWeaponDown(InputAction.CallbackContext Context)
        {
            if (_isPause)
                return;

            if (Context.started)
                _iShootPlayer.ChangeWeapon(WeaponDirection.Down);
        }

        public void SetPaused(IPaused iPause)
        {
            _iPaused = iPause;
            _iPaused.OnIsPause += SetStatusPause;
        }

        private void SetStatusPause(bool isPause) => _isPause = isPause;
    }
}
