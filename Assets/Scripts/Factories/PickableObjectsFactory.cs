using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Factories
{
    public class PickableObjectsFactory : ICreatePickableObjects
    {
        private readonly PickableObject.Factory _factory;
        private readonly Transform _parent;
        private readonly List<PickableObjectConfig> _list;

        public PickableObjectsFactory(PickableObject.Factory factory, Transform parent, List<PickableObjectConfig> list)
        {
            _factory = factory;
            _parent = parent;
            _list = list;
        }

        public void CreateAtPos(Transform spawn)
        { 
            var randomIndex = Random.Range(0, _list.Count);
            var prefab = _list[randomIndex].Prefab;
            var pickableObject = _factory.Create(prefab);
            pickableObject.transform.SetParent(_parent);
            pickableObject.transform.position = spawn.position;
        }
    }
}