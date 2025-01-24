using System.Collections.Generic;
using System.Linq;
using Factories;
using UnityEngine;
using Zenject;

namespace Items
{
    public class ItemsSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<Transform> spawnPoints;
        private ICreatePickableObjects _createPickableObjects;

        [Inject]
        public void Construct(ICreatePickableObjects createPickableObjects)
        {
            _createPickableObjects = createPickableObjects;
        }
        
        public void Initialize()
        {
            foreach (var spawn in spawnPoints.Where(x => x != null))
            {
                _createPickableObjects.CreateAtPos(spawn);
            }
        }
    }
}