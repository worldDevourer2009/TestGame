using Configs;
using UnityEngine;
using Zenject;

namespace Character
{
    public class PickUpController : MonoBehaviour, ITickable
    {
        [SerializeField] private Camera playerCam;
        [SerializeField] private Transform handPosition;
        [SerializeField] private PlayerConfig playerConfig;

        private GameObject _heldItem;
        private Vector2 _startPos;
        private Vector2 _endPos;

        public void Tick() =>
            HandleInput();

        private void HandleInput()
        {
            if (Input.touchCount == 0) return;

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    CheckPickapableObject(touch);
                    break;

                case TouchPhase.Ended:
                    _endPos = touch.position;

                    if (_heldItem == null) return;
                    var touchPosition = _endPos - _startPos;
                    if (touchPosition.magnitude <= 50f) break;
                    Throw(touchPosition);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                case TouchPhase.Canceled:
                default:
                    break;
            }
        }

        private void CheckPickapableObject(Touch touch)
        {
            var ray = playerCam.ScreenPointToRay(touch.position);
            if (!Physics.Raycast(ray, out RaycastHit hit, playerConfig.PickUpRange)) return;
            if (!hit.collider.CompareTag("Pickable")) return;
            PickUp(hit.collider.gameObject);
        }

        private void PickUp(GameObject item)
        {
            if (_heldItem != null) return;
            _heldItem = item;
            _heldItem.transform.position = handPosition.position;
            _heldItem.transform.localPosition = Vector3.zero;
            _heldItem.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void Throw(Vector2 direction)
        {
            var rb = _heldItem.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(new Vector3(direction.x, direction.y, 0).normalized * playerConfig.ThrowForce,
                ForceMode.Impulse);
            _heldItem = null;
        }
    }
}
