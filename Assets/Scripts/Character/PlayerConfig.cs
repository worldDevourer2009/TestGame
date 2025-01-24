using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Player Configs", fileName = "Player config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float throwForce;
        [SerializeField] private float pickUpRange;
        [SerializeField] private LayerMask pickableLayer;

        public float WalkSpeed => walkSpeed;
        public float PickUpRange => pickUpRange;
        public LayerMask PickLayer => pickableLayer;
        public float ThrowForce => throwForce;
    }
}