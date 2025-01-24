using System;
using UnityEngine;

namespace InputSystem
{
    public interface IInput
    {
        event Action<float> JoystickHorizontalInput;
        event Action<float> JoystickVerticalInput;
        event Action<Vector2> TouchPosition;
        event Action<Vector2> OnTap;
    }
}