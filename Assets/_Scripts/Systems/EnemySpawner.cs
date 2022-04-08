using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ISN
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _characterPosition;
        [SerializeField] private Transform _enemiesParent;
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private EnemySpawnSettings _enemySpawnSettings;
        [SerializeField] private ShootingPointsHandler _shootingPoints;
        [SerializeField] private CharacterNavigation _charNavigation;

        private ObjectsPool<Enemy> _pool;
        private List<EnemySpawnPoint> _spawnPoints;
        private int _currentEnemySpawnCounter;
        private int _shootingPointIndex;

        public event Action<int> EnemiesAtLocalSpawnDestroyed;
        public event Action<Enemy> OnEnemySpawn, 
                                   OnEnemyDestroy;

        private void Start()
        {
            _spawnPoints = _shootingPoints.GetEnemySpawnPoints();
            CreatePool(_enemySettings);
        }

        private void OnEnable() => _charNavigation.DestinationReached += OnDestinationReached;

        private void OnDisable() => _charNavigation.DestinationReached -= OnDestinationReached;

        private void CreatePool(EnemySettings enemy)
        {
            Enemy Create() => CreateEnemy(enemy, _enemiesParent);
            _pool = new ObjectsPool<Enemy>(Create, _enemySpawnSettings.enemyCountAtOneSpawn);
        }

        private Enemy CreateEnemy(EnemySettings enemySettings, Transform parent)
        {
            var gameObject = Instantiate(enemySettings.EnemyPrafab, parent);
            return gameObject.GetComponent<Enemy>();
        }

        private void OnDestinationReached(int shootingPointIndex)
        {
            _shootingPointIndex = shootingPointIndex;
            _currentEnemySpawnCounter = _enemySpawnSettings.enemyCountAtOneSpawn;
            for (int i = 0; i < _currentEnemySpawnCounter; i++)
                SpawnEnemy(_spawnPoints[shootingPointIndex].transform);
        }

        private void SpawnEnemy(Transform currentSpawnPoint)
        {
            Enemy enemy = _pool.TakeObject();
            enemy.transform.position = GetRandomPosition(currentSpawnPoint);
            enemy.transform.LookAt(_characterPosition);
            enemy.SetParameters(_enemySettings);
            OnEnemySpawn?.Invoke(enemy);
            enemy.HealthIsOver += OnHealthIsOver;
        }
        private Vector3 GetRandomPosition(Transform spawnPoint)
        {
            Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle.normalized;
            Vector3 direction = new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y);
            return spawnPoint.position + direction * _enemySpawnSettings.distanceFromSpawnPoint;
        }

        private void OnHealthIsOver(Enemy enemy)
        {
            enemy.HealthIsOver -= OnHealthIsOver;
            OnEnemyDestroy?.Invoke(enemy);

            StartCoroutine(PutEnemyInPoolAfterTime(enemy));

            _currentEnemySpawnCounter -= 1;
            if (_currentEnemySpawnCounter == 0)
                EnemiesAtLocalSpawnDestroyed?.Invoke(_shootingPointIndex + 1);
        }

        private IEnumerator PutEnemyInPoolAfterTime(Enemy enemy)
        {
            yield return new WaitForSeconds(2.0f);
            _pool.PutObjectBack(enemy);
        }
    }
}
