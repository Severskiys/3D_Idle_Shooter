using System;
using UnityEngine;

namespace ISN
{
    [RequireComponent(typeof(Animator))]
    public class AnimationSwitch : MonoBehaviour
    {
        [SerializeField] private CharacterNavigation _navigation;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _navigation.DestinationReached += OnDestinationReached;
            _navigation.MovementStarted += OnMovementStarted;
        }

        private void OnDisable()
        {
            _navigation.DestinationReached -= OnDestinationReached;
            _navigation.MovementStarted -= OnMovementStarted;
        }

        private void OnMovementStarted() => _animator.SetTrigger("Running");

        private void OnDestinationReached(int obj) => _animator.SetTrigger("Idle");
    }
}

