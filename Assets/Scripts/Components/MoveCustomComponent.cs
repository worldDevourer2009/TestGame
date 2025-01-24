using System;
using Configs;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Components
{
    public class MoveCustomComponent : IInitializable, IMoveCustom, IDisposable
    {
        private const float Gravity = 9.81f;
        
        private readonly IInput _input;
        private readonly CharacterController _characterController;
        private readonly PlayerConfig _playerConfig;
        private readonly Transform _transform;

        private float Speed => _playerConfig.WalkSpeed;
        private float _verticalInput;
        private float _horizontalInput;

        public MoveCustomComponent(IInput input, CharacterController characterController, PlayerConfig playerConfig)
        {
            _input = input;
            _characterController = characterController;
            _playerConfig = playerConfig;

            if (_characterController == null)
            {
                Debug.Log("Character controller was not initialized");
                return;
            }
            
            _transform = _characterController.gameObject.transform;
        }

        public void Initialize()
        {
            _input.JoystickHorizontalInput += HandleHorizontalInput;
            _input.JoystickVerticalInput += HandleVerticalInput;
        }

        public void Move()
        {
            var moveDirection = _transform.forward * _verticalInput + _transform.right * _horizontalInput;
            moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move(moveDirection * Speed * Time.deltaTime);
        }

        private void HandleHorizontalInput(float direction) => 
            _horizontalInput = direction;
        
        private void HandleVerticalInput(float direction) =>
            _verticalInput = direction;

        public void Dispose()
        {
            _input.JoystickHorizontalInput -= HandleHorizontalInput;
            _input.JoystickHorizontalInput -= HandleVerticalInput;
        }
    }
}