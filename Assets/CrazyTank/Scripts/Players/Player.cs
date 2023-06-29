using CrazyTank.Data;
using CrazyTank.Input;
using CrazyTank.Interface;
using CrazyTank.Weapons;
using System.Collections;
using UnityEngine;

namespace CrazyTank.Characters
{
    public sealed class Player : BaseCharacter, IMoveble, IArmed
    {
        private WeaponHandler _weaponHandler;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private Transform _gunPoint;

        private Bullet _bullet;
        private GameObject _gun;
        private float _currentSpeedBullet;
        private float _currentDamageBullet;

        [SerializeField] private Rigidbody _rigidbody;
        private Vector3 _currentDirection;
        private float _speedRotate = 30f;
        private float _timeUpdatePosition = 0.5f;

        private void Awake()
        {
            characterType = CharacterType.Player;
            base.iArmed = this;
        }

        private void FixedUpdate()
        {
            if (!base.gameOn)
                return;

            MovePlayer();
            Rotate();
        }

        private void OnDestroy()
        {
            Destroy(gameObject);
        }

        public void Move(Vector3 direction, bool isOn)
        {
            if (isOn)
                _currentDirection.z = direction.z;
            else
                _currentDirection.z = 0f;
        }

        public void Rotate(Vector3 direction, bool isOn)
        {
            if (isOn)
                _currentDirection.x = direction.x;
            else
                _currentDirection.x = 0f;
        }

        private void MovePlayer()
        {
            Vector3 movPosition = transform.forward * (_currentDirection.z * Speed * Time.deltaTime);
            _rigidbody.MovePosition(_rigidbody.position + movPosition);
        }

        private void Rotate()
        {
            float angle = _currentDirection.x * _speedRotate * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * rotation);
        }

        public void ChangeWeapon(WeaponDirection direction)
        {
            Weapon data = _weaponHandler.GetWeapon(direction);
            _currentSpeedBullet = data.Speed;
            _currentDamageBullet = data.Damage;
            ChangeWeapon(data.GunPrefab, data.BulletPrefab);
        }

        private void ChangeWeapon(GameObject gun, Bullet bullet)
        {
            SetUpGun(gun);
            _bullet = bullet;
        }

        public void Fire(PressedStatus status)
        {
            Bullet bullet = Instantiate(_bullet, _firePoint).GetComponent<Bullet>();
            bullet.SetData(_currentSpeedBullet, _currentDamageBullet);
        }

        private IEnumerator UpdatePosition()
        {
            while (gameOn)
            {
                iSpawner.SetTarget(transform.position);
                yield return new WaitForSeconds(_timeUpdatePosition);
            }
        }

        private void SetUpGun(GameObject newGun)
        {
            Destroy(_gun);
            _gun = Instantiate(newGun, _gunPoint);
            _gun.transform.SetParent(_gunPoint, true);
        }

        public void SetStatusGame(bool isPlay)
        {
            gameOn = isPlay;

            if (gameOn)
                StartCoroutine(UpdatePosition());
            else
                StopAllCoroutines();
        }

        public void SetWeaponsConfiguration(Weapon[] _weapons, IDisplaying displaying)
        {
            _weaponHandler = new WeaponHandler(_weapons, displaying);
            ChangeWeapon(WeaponDirection.Down);
        }
    }
}