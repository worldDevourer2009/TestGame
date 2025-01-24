using UnityEngine;

namespace Components
{
    public interface IPickable
    {
        void Grab(Transform grabPoint);
    }
}