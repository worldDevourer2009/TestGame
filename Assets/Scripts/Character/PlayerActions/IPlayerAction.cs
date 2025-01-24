using System;
using UnityEngine;

namespace Character
{
    public interface IPlayerAction
    {
        event Action<float> OnPlayerMoveHorizontal;
        event Action<float> OnPlayerMoveVertical;
        event Action<Vector2> OnTap;
    }
}