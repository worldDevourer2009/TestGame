using Components;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PlayerController : MonoBehaviour, ITickable
    {
        [SerializeField] private PlayerConfig playerConfig;
        private float Speed => playerConfig.WalkSpeed;
        private IMoveCustom _moveCustom;
        private IPickUp _pickUp;

        [Inject]
        public void Construct(IMoveCustom moveCustom, IPickUp pickUp)
        {
            _moveCustom = moveCustom;
            _pickUp = pickUp;
        }

        public void Tick()
        {
            _moveCustom.Move(Speed);
            _pickUp.PickUp();
        }
    }
}