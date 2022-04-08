using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISN
{
    public class UIHelthBarSpawner : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private EnemySpawnSettings _enemySpawnSettings;
        [SerializeField] private UIHealthBarView _UIprefab;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Transform _uIParent;

        private ObjectsPool<UIHealthBarView> _pool;
        private Dictionary<Enemy, UIHealthBarView> _hPBarAtEnemy;

        private void Start()
        {
            UIHealthBarView Create() => Instantiate(_UIprefab, _uIParent);
            _pool = new ObjectsPool<UIHealthBarView>(Create, _enemySpawnSettings.enemyCountAtOneSpawn);
            _hPBarAtEnemy = new Dictionary<Enemy, UIHealthBarView>();
        }

        private void OnEnable()
        {
            _enemySpawner.OnEnemySpawn += OnEnemySpawned;
            _enemySpawner.OnEnemyDestroy += OnEnemyDestroyed;
        }

        private void OnDisable()
        {
            _enemySpawner.OnEnemySpawn -= OnEnemySpawned;
            _enemySpawner.OnEnemyDestroy -= OnEnemyDestroyed;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            var view = _pool.TakeObject();
            view.Init(enemy, _mainCamera, enemy.MaxHealth);
            _hPBarAtEnemy[enemy] = view;
        }

        private void OnEnemyDestroyed(Enemy enemy)
        {
            var view = _hPBarAtEnemy[enemy];
            _pool.PutObjectBack(view);
        }
    }
}

