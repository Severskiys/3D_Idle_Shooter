using System;
using UnityEngine;

namespace ISN
{
    public class TouchInput : MonoBehaviour
    {
        private Touch _touch;
        public event Action<Vector2> TouchDone;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);
                if (_touch.phase == TouchPhase.Began)
                {
                    TouchDone?.Invoke(_touch.position);
                }
            }
        }
    }
}

