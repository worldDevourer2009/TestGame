using InputSystem;
using UnityEngine;
using Zenject;

namespace Components
{
    //нерабочая часть кода, что то менялось, что то оставалось, проблема кода,
    //что нормально не догадался как считывать значения из InputManager здесь
    //чтобы правильно воспринимала игра мои нажатия
    public class PickUpComponent : IPickUp, IInitializable, ITickable
    {
        private readonly IInput _input;
        private readonly Transform _pickTransform;
        
        private readonly Camera _camera;
        
        //можно было бы через Scriptable Object добавить эти параметры, а не передавать их вручную
        private readonly float _searchRadius;
        private readonly float _distance;
        private readonly float _height;
        
        private GameObject _heldObject;
        private Vector2 _searchDirection;
        private Vector2 _dragDirection;
        private bool _canPick;
        private bool _isHoldingObject;

        public PickUpComponent(IInput input, 
            Camera camer,
            Transform pickTransform, 
            float searchRadius, 
            float distance, 
            float height)
        {
            _input = input;
            _camera = camer;
            _pickTransform = pickTransform;
            _searchRadius = searchRadius;
            _distance = distance;
            _height = height;
        }

        public void Initialize()
        {
            _input.OnTap += HandleInput;
            Debug.Log("Hello");
        }
        
        public void Tick()
        {
            if (_camera != null)
            {
                var worldPoint = _camera.ScreenToWorldPoint(
                    new Vector3(_searchDirection.x, _searchDirection.y, _camera.nearClipPlane));
                
                _dragDirection = new Vector2(worldPoint.x, worldPoint.z);
            }
                
            if (!_isHoldingObject) return;

            _heldObject.transform.position = Vector3.Lerp(
                _heldObject.transform.position, 
                _pickTransform.position, 
                Time.fixedDeltaTime * 10f);
        }

        private void HandleInput(Vector2 direction)
        {
            _searchDirection = direction.normalized;
            
            if (!_isHoldingObject)
                TryGrabObject();
            else
                Throw();
        }

        public void PickUp()
        {
            if (_isHoldingObject) return;
            
            if (_heldObject == null) return;
            SetPosition();
        }

        private void SetPosition()
        {
            _heldObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _heldObject.transform.position = _pickTransform.position 
                                             + _distance * _pickTransform.forward 
                                             + _height * _pickTransform.up;
        }

        private bool TryGrabObject()
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, _searchRadius))
                return _canPick = true;

            return _canPick = false;
        }

        public void Throw()
        {
            if (!_isHoldingObject || _heldObject == null) return;
            _heldObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            _heldObject = null;
        }
    }
}