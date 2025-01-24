using System;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class InputManager : IInput, ITickable, IFixedTickable
    {
        public event Action<Vector2> TouchPosition = delegate { };
        public event Action<Vector2> TapPosition = delegate { };
        public event Action<float> JoystickHorizontalInput = delegate { };
        public event Action<float> JoystickVerticalInput = delegate { };

        private readonly Joystick _joystick;

        private bool _isTouching;
        private Vector2 _lastTouchPos = Vector2.zero;

        public InputManager(Joystick joystick)
        {
            _joystick = joystick;
        }
        
        public void FixedTick()
        {
            HandleJoyStickInput();
        }

        public void Tick()
        {
            HandleTouch();
            HandleTap();
        }
        
        private void HandleJoyStickInput()
        {
            var horizontal = _joystick.Horizontal;
            var vertical = _joystick.Vertical;
            
            JoystickHorizontalInput?.Invoke(horizontal);
            JoystickVerticalInput?.Invoke(vertical);
        }

        private void HandleTap()
        {
            if (Input.touchCount == 0) return;
            var touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began || _isTouching) return;
            TapPosition?.Invoke(touch.position);
        }

        private void HandleTouch()
        {
            if (Input.touchCount == 0) return;
            var touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    _isTouching = true;
                    var dif = touch.position - _lastTouchPos;
                    TouchPosition?.Invoke(dif.normalized);
                    _lastTouchPos = touch.position;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    _isTouching = false;
                    break;
                case TouchPhase.Began:
                default:
                    break;
            }
        }
    }
}