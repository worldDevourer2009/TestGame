using System;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Components
{
    public class CameraFollowCustomComponent : ICameraFollow, IInitializable, IDisposable
    {
        private const float Noise = 0.05f;
        
        private readonly IInput _input;
        private readonly CameraConfig _cameraConfig;
        private readonly Camera _camera;
        private readonly Transform _playerTransform;

        private float _rotationX;
        private Vector2 _touchPos = Vector2.zero;
        private Vector2 _lastTouchPosition = Vector2.zero;
        
        private Vector3 _lastRotation = Vector3.zero;
        
        private bool _canMove;

        public CameraFollowCustomComponent(IInput input, CameraConfig cameraConfig, Camera camera, Transform playerTransform)
        {
            _input = input;
            _cameraConfig = cameraConfig;
            _camera = camera;
            _playerTransform = playerTransform;
        }

        public void Initialize()
        {
            _input.TouchPosition += HandleTouch;
        }
        
        public void Follow()
        {
            if (!_canMove) return;
            
            var posX = _lastTouchPosition.x;
            var posY = _lastTouchPosition.y;

            var newPos = new Vector3(posX, posY).normalized;
            
            if (newPos == _lastRotation) return;
            _lastRotation = newPos;
            
            _playerTransform.Rotate(Vector3.up * newPos.x * _cameraConfig.Sensitivity, Space.World);

            _rotationX -= newPos.y * _cameraConfig.Sensitivity;  
            
            Debug.Log($"New rotation is {_rotationX}");
            
            _rotationX = Mathf.Clamp(_rotationX, -_cameraConfig.MaxAngleY, _cameraConfig.MaxAngleY);
            _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }
        
        private void HandleTouch(Vector2 position)
        {
            if (_lastTouchPosition == position)
            {
                _canMove = false;
                return;
            }
            
            _touchPos = position;
            
            if (_touchPos.magnitude < Noise) return;
            
            _canMove = true;
            _lastTouchPosition = _touchPos;
        }

        public void Dispose()
        {
            _input.TouchPosition -= HandleTouch;
        }
    }
}