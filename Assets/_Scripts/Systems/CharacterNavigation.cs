using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace ISN
{
    public class CharacterNavigation : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private ShootingPointsHandler _shootingPoints;
        [SerializeField] private EnemySpawner _enemySpawner;

        private List<CharacterPosition> _targets;
        private Vector3 _currentDestination;
        private int _shootingPointIndex = 0;
        private int _frameCounter = 0;
        private bool _destinationNotReached = true;

        public event Action<int> DestinationReached;
        public event Action MovementStarted;

        private void OnEnable()
        {
            _targets = _shootingPoints.GetCharacterPositions();
            StartMoving();
            _enemySpawner.EnemiesAtLocalSpawnDestroyed += OnEnemiesAtLocalSpawnDestroyed;
        }

        private void OnDisable() => _enemySpawner.EnemiesAtLocalSpawnDestroyed -= OnEnemiesAtLocalSpawnDestroyed;


        private void Update() => UpdateEveryNFrames();

        private void UpdateEveryNFrames()
        {
            _frameCounter++;
            if (_frameCounter == 30)
            {
                _frameCounter = 0;
                CheckIsDestinationReached();
            }
        }

        private void CheckIsDestinationReached()
        {
            if (_agent.remainingDistance < 0.35f && _destinationNotReached)
            {
                _destinationNotReached = false;
                DestinationReached?.Invoke(_shootingPointIndex);
            }
        }

        private void StartMoving()
        {
            MovementStarted?.Invoke();
            _currentDestination = _targets[_shootingPointIndex].transform.position;
            _agent.SetDestination(_currentDestination);
        }

        private void OnEnemiesAtLocalSpawnDestroyed(int shootingPointIndex)
        {
            _shootingPointIndex = shootingPointIndex;
            if (_shootingPointIndex == _targets.Count)
            {
                ReloadScene();
                return;
            }
            StartMoving();
            _destinationNotReached = true;
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}