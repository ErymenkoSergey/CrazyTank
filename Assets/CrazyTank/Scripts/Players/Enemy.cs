using CrazyTank.Data;
using CrazyTank.Interface;
using System.Collections;
using UnityEngine;

namespace CrazyTank.Characters
{
    [RequireComponent(typeof(AIModule))]
    public sealed class Enemy : BaseCharacter, IFollowing
    {
        [SerializeField] private GameObject _aI;
        private ISimulated _iAI;

        private void Awake()
        {
            characterType = CharacterType.Enemy;
            base.iFollowing = this;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            if (_aI.TryGetComponent(out ISimulated ai))
            {
                _iAI = ai;
                _iAI.SetStatusAtack(gameOn);
                _iAI.SetSpeed(base.Speed);

                iSpawner.OnGetPosition += CheckTarget;
            }
        }
        private void OnDestroy()
        {
            iSpawner.OnGetPosition -= CheckTarget;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageble damageble))
            {
                damageble.TakeDamage(Damage);
            }
        }

        public void ChangeStatus(bool isOn) => gameOn = isOn;
        private void CheckTarget(Vector3 vector) => _iAI.SetTarget(vector);
    }
}