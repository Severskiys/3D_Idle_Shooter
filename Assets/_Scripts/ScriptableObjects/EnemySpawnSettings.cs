using UnityEngine;

namespace ISN
{
    [CreateAssetMenu(fileName = "EnemySpawnSettings", menuName = "ISN/EnemySpawnSettings")]
    public class EnemySpawnSettings : ScriptableObject
    {
        [Header("Enemy Spawn Settings")]
        [Min(1)] public float distanceFromSpawnPoint;
        [Min(0)] public int enemyCountAtOneSpawn = 3;
    }
}
