using UnityEngine;

namespace Factories
{
    public interface ICreatePickableObjects
    {
        void CreateAtPos(Transform spawnPoints);
    }
}