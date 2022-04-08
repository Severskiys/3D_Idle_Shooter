using UnityEngine;

namespace ISN
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ISN/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [Header("Enemy Settings")]
        [Min(0)] public float EnemyHP;
        public GameObject EnemyPrafab;
    }
}