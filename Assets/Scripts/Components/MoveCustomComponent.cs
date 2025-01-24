using System;
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
        private readonly Transform _transform;
        
        private float _verticalInput;
        private float _horizontalInput;

        public MoveCustomComponent(IInput input, CharacterController characterController)
        {
            _input = input;
            _characterController = characterController;

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
            _input.JoystickHorizontalInput += HandleVerticalInput;
            
            Debug.Log("Init MoveCustomComponent");
        }

        public void Move(float speed)
        {
            Debug.Log($"Horizontal input is {_horizontalInput} and vertical is {_verticalInput}");
            var moveDirection = _transform.forward * _verticalInput + _transform.right * _horizontalInput;
            moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move(moveDirection * speed * Time.deltaTime);
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