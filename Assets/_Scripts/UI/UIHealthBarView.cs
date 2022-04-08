using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace ISN
{
    public class UIHealthBarView : MonoBehaviour
    {
        [SerializeField] private Vector3 _hPBarOffset;
        [SerializeField] private TMP_Text _healtText;
        [SerializeField] private Image _healthBG;

        private Enemy _target;
        private Camera _mainCamera;
        private float _maxHealthValue;

        public void Init(Enemy target, Camera camera, float maxHPAmount)
        {
            _healtText.text = maxHPAmount.ToString();
            _healthBG.fillAmount = 1;

            _maxHealthValue = maxHPAmount;
            _mainCamera = camera;
            _target = target;

            _target.HealthChanged += OnHealthChanged;
            _target.HealthIsOver += OnHealthIsOver;

            SetPosition();
        }

        private void OnHealthIsOver(Enemy enemy)
        {
            enemy.HealthChanged -= OnHealthChanged;
            enemy.HealthIsOver -= OnHealthIsOver;
        }

        private void OnHealthChanged(float healthValue)
        {
            _healtText.text = healthValue.ToString();
            var fillAmount = healthValue / _maxHealthValue;
            _healthBG.fillAmount = fillAmount;
        }

        private void SetPosition() =>
            transform.position = _mainCamera.WorldToScreenPoint(_target.transform.position) + _hPBarOffset;
    }
}
