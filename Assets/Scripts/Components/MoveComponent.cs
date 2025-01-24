using UnityEngine;

namespace Components
{
    public class MoveComponent : IMove
    {
        private readonly Rigidbody _rb;
        
        public MoveComponent(Rigidbody rb)
        {
            _rb = rb;
        }

        public void Move(Vector3 direction, float speed)
        {
            _rb.velocity = direction * speed;
        }
    }
}