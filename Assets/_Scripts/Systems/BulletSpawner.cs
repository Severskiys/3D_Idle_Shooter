using System.Collections;
using UnityEngine;

namespace ISN
{
    [RequireComponent(typeof(TouchInput))]
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Camera _cameraMain;
        [SerializeField] private Transform _bulletSpawnTransform;
        [SerializeField] private BulletSettings _bulletSettings;

        private ObjectsPool<Bullet> _pool;
        private TouchInput _input;

        private void Awake() => _input = GetComponent<TouchInput>();

        private void Start() => CreatePool();

        private void OnEnable() => _input.TouchDone += OnTouchDone;

        private void OnDisable() => _input.TouchDone -= OnTouchDone;

        private void CreatePool()
        {
            Bullet Create() => CreateBullet();
            _pool = new ObjectsPool<Bullet>(Create, _bulletSettings.PoolSize);
        }

        private Bullet CreateBullet()
        {
            var gameObject = Instantiate(_bulletSettings.BulletPrefab, _bulletSpawnTransform);
            return gameObject.GetComponent<Bullet>();
        }

        private void OnTouchDone(Vector2 touchPosition)
        {
            var ray = _cameraMain.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                Bullet bullet = _pool.TakeObject();
                bullet.transform.position = _bulletSpawnTransform.position;
                bullet.SetParameters(_bulletSettings);
                bullet.BulletCollided += OnBulletCollided;
                StartCoroutine(BulletFlight(hitInfo.point, bullet.transform));
            }
        }

        private void OnBulletCollided(Bullet bullet)
        {
            bullet.BulletCollided -= OnBulletCollided;
            _pool.PutObjectBack(bullet);
        }

        private IEnumerator BulletFlight(Vector3 bulletTarget, Transform bulletTransform)
        {
            var bullet = bulletTransform.gameObject;
            while (bullet.activeInHierarchy)
            {
                bulletTransform.position = Vector3.MoveTowards(bulletTransform.position, bulletTarget, Time.deltaTime * _bulletSettings.BulletSpeed);
                yield return null;
            }
        }
    }
}


