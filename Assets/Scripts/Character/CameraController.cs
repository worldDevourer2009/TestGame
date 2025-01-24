using Components;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CameraController : MonoBehaviour, ITickable
    {
        private ICameraFollow _cameraFollow;
        
        [Inject]
        public void Construct(ICameraFollow cameraFollow)
        {
            _cameraFollow = cameraFollow;
        }

        void ITickable.Tick()
        {
            _cameraFollow.Follow();
        }
    }
}