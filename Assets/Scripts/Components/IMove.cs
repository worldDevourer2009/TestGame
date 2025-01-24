using UnityEngine;

namespace Components
{
    public interface IMove
    {
        void Move(Vector3 direction, float speed);
    }
}