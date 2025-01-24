using Components;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CameraController : MonoBehaviour
    {
        [Inject]
        public void Construct(ICameraFollow cameraFollow)
        {
        }
    }
}