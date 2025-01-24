using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "PickablesObjects", fileName = "PickableObjectConfig", order = 0)]
    public class PickableObjectConfig : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}