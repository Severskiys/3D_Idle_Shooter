using System;
using UnityEngine;

namespace ISN
{
    public class Enemy : MonoBehaviour
    {
        private float _health;
        public float MaxHealth { get; private set;}

        public event Action<Enemy> HealthIsOver;
        public event Action<float> HealthChanged;

        public Enemy SetParameters(EnemySettings settings)
        {
            MaxHealth = settings.EnemyHP;
            _health = settings.EnemyHP;
            HealthChanged?.Invoke(_health);
            return this;
        }

        public void TakeDamage(float damageAmount)
        {
            _health -= damageAmount;
            HealthChanged?.Invoke(_health);
            if (_health <= 0)
                HealthIsOver?.Invoke(this);
        }
    }
}

