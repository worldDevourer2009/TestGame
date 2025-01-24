using System;
using Configs;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Components
{
    public class CameraFollowCustomComponent : ICameraFollow, IInitializable, IDisposable
    {
        private readonly IInput _input;
        private readonly CameraConfig _cameraConfig;
        private readonly Camera _camera;
        private readonly Transform _playerTransform;

        private float _rotationX;
        private float _rotationY;

        public CameraFollowCustomComponent(IInput input, 
            CameraConfig cameraConfig, 
            Camera camera, 
            Transform playerTransform)
        {
            _input = input;
            _cameraConfig = cameraConfig;
            _camera = camera;
            _playerTransform = playerTransform;
        }

        public void Initialize()
        {
            _input.TouchPosition += Follow;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("inint comp");
        }

        public void Follow(Vector2 touchPosition)
        {
            _rotationX -= touchPosition.y * _cameraConfig.Sensitivity * Time.deltaTime; 
            _rotationY += touchPosition.x * _cameraConfig.Sensitivity * Time.deltaTime;
            
            _rotationX = Mathf.Clamp(_rotationX, -_cameraConfig.MaxAngleY, _cameraConfig.MaxAngleY);
            _camera.transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0f);
            _playerTransform.rotation = Quaternion.Euler(0f, _rotationY, 0f);
        }

        public void Dispose()
        {
            _input.TouchPosition -= Follow;
        }
    }
}