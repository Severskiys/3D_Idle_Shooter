using System;
using TMPro;
using UnityEngine;

namespace ISN
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private TouchInput _input;
        [SerializeField] private CharacterNavigation _characterNavigation;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private TMP_Text _startText;

        private void Awake()
        {
            _bulletSpawner.enabled = false;
            _characterNavigation.enabled = false;
        }
        private void OnEnable() => _input.TouchDone += OnTouchDone;

        private void OnTouchDone(Vector2 obj)
        {
            _input.TouchDone -= OnTouchDone;
            _startText.enabled = false;
            _characterNavigation.enabled = true;
            _bulletSpawner.enabled = true;
        }
    }
}

