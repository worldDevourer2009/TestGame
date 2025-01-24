using System;
using Character;
using InputSystem;
using Items;
using UnityEngine;
using Zenject;

namespace Components
{
    public class PickUpComponent : IPickUp, IInitializable, IFixedTickable, IDisposable
    {
        private readonly IInput _input;
        private readonly PlayerConfig _playerConfig;
        private readonly Camera _camera;
        private readonly Transform _holdTransform;
        
        private float Range => _playerConfig.PickUpRange;
        private float ThrowForce => _playerConfig.ThrowForce;
        private LayerMask PickUpLayer => _playerConfig.PickLayer;
        
        private Rigidbody _heldObject;
        private Vector2 _direction;

        public PickUpComponent(IInput input, PlayerConfig playerConfig, Transform holdTransform, Camera camera)
        {
            _input = input;
            _playerConfig = playerConfig;
            _holdTransform = holdTransform;
            _camera = camera;
        }

        public void Initialize()
        {
            _input.TapPosition += HandleTap;
        }
        
        public void FixedTick()
        {
            if (_heldObject != null) return;

            var pickableObject = GetPickableObject();
            
            if (pickableObject != null)
                _direction = Vector2.zero;
        }
        
        private void HandleTap(Vector2 direction) => _direction = direction;

        public void PickUp()
        {
            if (_heldObject != null) return;
            
            var obj = GetPickableObject();
            if (obj == null) return;

            PickUpObject(obj);
        }

        private void PickUpObject(PickableObject obj)
        {
            if (!obj.gameObject.TryGetComponent<Rigidbody>(out var comp)) return;
            
            _heldObject = comp;
            _heldObject.isKinematic = true;
            _heldObject.transform.parent = _holdTransform.transform;
            
            var player = _holdTransform.transform.parent.gameObject;
            
            Physics.IgnoreCollision(_heldObject.GetComponent<Collider>(), 
                player.GetComponent<Collider>(), true);
        }

        private PickableObject GetPickableObject()
        {
            var ray = _camera.ScreenPointToRay(_direction);
            var hits = Physics.RaycastAll(ray, Range, PickUpLayer);
            
            foreach (var hit in hits)
            {
                Debug.Log($"HIt name {hit.collider.gameObject.name}");
                if (!hit.collider.TryGetComponent<PickableObject>(out var comp)) continue;
                return comp;
            }

            return null;
        }

        public void Throw()
        {
            var player = _holdTransform.transform.parent.gameObject;
            
            Physics.IgnoreCollision(_heldObject.GetComponent<Collider>(), 
                player.GetComponent<Collider>(), false);
            
            _heldObject.isKinematic = false;
            _heldObject.transform.parent = null;
            _heldObject.AddForce(_holdTransform.forward * ThrowForce);
            _heldObject = null;
        }

        public void Dispose()
        {
            _input.TapPosition -= HandleTap;
        }

        private bool CanPickUp()
        {
            var hits = new RaycastHit[10];
            foreach (var hit in hits)
            {
                if (Vector3.Distance(_holdTransform.position, hit.collider.transform.position) < Range) continue;
                return true;
            }
            
            return false;
        }
    }
}