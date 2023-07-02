using System.Collections;
using UnityEngine;

namespace CrazyTank.Weapons
{
    public sealed class Bullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private float _timeDestroy = 4f;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_timeDestroy);
            Destroy();
        }

        public void SetData(float speed, float damage)
        {
            _speed = speed;
            _damage = damage;
            transform.SetParent(null);
        }

        private void Update()
        {
            transform.Translate(0, 0, _speed * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamageble damageble))
            {
                damageble.TakeDamage(_damage);
                Destroy();
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}