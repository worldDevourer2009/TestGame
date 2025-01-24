using System;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerAction : IPlayerAction, IInitializable
    {
        public event Action<float> OnPlayerMoveHorizontal;
        public event Action<float> OnPlayerMoveVertical;
        public event Action<Vector2> OnTap;
        private readonly IInput _input;

        public PlayerAction(IInput input)
        {
            _input = input;
        }
        public void Initialize()
        {
        }

        private void OnMoveVertical(float direction)
        {
            OnPlayerMoveVertical?.Invoke(direction);
        }

        private void OnMoveHorizontal(float direction)
        {
            OnPlayerMoveHorizontal?.Invoke(direction);
        }

        private void OnTapDirection(Vector2 direction)
        {
            OnTap?.Invoke(direction);
        }
    }
}