using Components;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerController : MonoBehaviour, ITickable
    {
        private IMoveCustom _moveCustom;

        [Inject]
        public void Construct(IMoveCustom moveCustom)
        {
            _moveCustom = moveCustom;
        }
        
        public void Tick()
        {
            _moveCustom.Move();
        }
    }
}