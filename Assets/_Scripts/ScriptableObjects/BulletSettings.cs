using UnityEngine;

namespace ISN
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "ISN/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [Header("Bullet Settings")]
        [Min(0)] public int BulletDamage;
        [Min(0)] public int BulletSpeed;
        [Min(0)] public int PoolSize = 30;
        public GameObject BulletPrefab;
    }
}
