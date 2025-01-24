using System;
using UnityEngine;
using Zenject;

namespace Input
{
    public class InputManager : IInput, ITickable, IFixedTickable
    {
        public event Action<Vector2> TouchPosition = delegate { };
        public event Action<Vector2> OnTap = delegate { };
        public event Action<float> JoystickHorizontalInput = delegate { };
        public event Action<float> JoystickVerticalInput = delegate { };
        

        private readonly Joystick _joystick;
        private readonly Camera _camera;

        private bool _isTouching;
        private float _fingerID;
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
        }
        
        private void HandleJoyStickInput()
        {
            var horizontal = _joystick.Horizontal;
            var vertical = _joystick.Vertical;
            
            JoystickHorizontalInput?.Invoke(horizontal);
            JoystickVerticalInput?.Invoke(vertical);
        }

        private void HandleTouch()
        {
            if (Input.touchCount == 0)
            {
                if (!_isTouching) return;
                _isTouching = false;
                return;
            }
            
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (_isTouching) break;
                    _fingerID = touch.fingerId;
                    
                    OnTap?.Invoke(touch.position);
                    _isTouching = true;
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isTouching = false;
                    OnTap?.Invoke(Vector2.zero);
                    TouchPosition?.Invoke(Vector2.zero);
                    break;
                case TouchPhase.Stationary:
                    if (!_isTouching) break;
                    _lastTouchPos = Vector2.zero;
                    OnTap?.Invoke(touch.position);
                    TouchPosition?.Invoke(_lastTouchPos);
                    break;
                case TouchPhase.Moved:
                    if (!_isTouching) break;

                    foreach (var tch in Input.touches)
                    {
                        if (tch.fingerId != _fingerID) continue;
                        _lastTouchPos = touch.deltaPosition;
                        OnTap?.Invoke(touch.position);
                        TouchPosition?.Invoke(_lastTouchPos); 
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}