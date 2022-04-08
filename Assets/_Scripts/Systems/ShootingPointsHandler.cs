using System.Collections.Generic;
using UnityEngine;

namespace ISN
{

public class ShootingPointsHandler : MonoBehaviour
{
    [SerializeField] private ShootingPoint[] _shootingPoints;

    private List<EnemySpawnPoint> _spawnPoints = new List<EnemySpawnPoint>();
    private List<CharacterPosition> _characterPositions = new List<CharacterPosition>();

    private void Awake()
    {
        foreach (ShootingPoint point in _shootingPoints)
        {
            _spawnPoints.Add(point.GetComponentInChildren<EnemySpawnPoint>());
            _characterPositions.Add(point.GetComponentInChildren<CharacterPosition>());
        }
    }

    public List<EnemySpawnPoint> GetEnemySpawnPoints()
    {
        return _spawnPoints;
    }

    public List<CharacterPosition> GetCharacterPositions()
    {
        return _characterPositions;
    }
}
}
