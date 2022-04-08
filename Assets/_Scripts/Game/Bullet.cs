using System;
using UnityEngine;

namespace ISN
{
    public class Bullet : MonoBehaviour
    {
        private float _damage;

        public event Action<Bullet> BulletCollided;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);

            BulletCollided?.Invoke(this);
        }

        public void SetParameters(BulletSettings settings)
        {
            _damage = settings.BulletDamage;
        }
    }
}

