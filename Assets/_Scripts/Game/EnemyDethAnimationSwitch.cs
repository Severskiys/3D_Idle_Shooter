using UnityEngine;

namespace ISN
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(Animator))]
    public class EnemyDethAnimationSwitch : MonoBehaviour
    {
        private Enemy _enemy;
        private Animator _animator;
        private int _randomAnimId = 0;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable() => _enemy.HealthIsOver += OnHealthIsOver;

        private void OnHealthIsOver(Enemy enemy)
        {
            _enemy.HealthIsOver -= OnHealthIsOver;
            _randomAnimId = UnityEngine.Random.Range(1, 4);
            PlayRandomAnimation();
        }

        private void PlayRandomAnimation()
        {
            switch(_randomAnimId)
            {
                case 1:
                    _animator.SetTrigger("Death1");
                    break;
                case 2:
                    _animator.SetTrigger("Death2");
                    break;
                case 3:
                    _animator.SetTrigger("Death3");
                    break;
            }
        }
    }
}

