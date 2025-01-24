using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Items
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        public class Factory : PlaceholderFactory<Object, PickableObject> {}
    }
}