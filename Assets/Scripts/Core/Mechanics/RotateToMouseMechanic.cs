using System;
using Core.Components;
using Core.Entity;
using UnityEngine;

namespace Core.Mechanics
{
    [Serializable]
    public class RotateToMouseMechanic
    {
        private const float MAX_VERTICAL_OFFSET = 40f;

        public event Action<Quaternion> Rotated;
        
        [SerializeField] private float _mouseSensitivity;

        private float _xRotation;
        private float _yRotation;
        
        public void Initialize(Quaternion initialRotation)
        {
            var currentRotation = initialRotation;
            
            _xRotation = currentRotation.x;
            _yRotation = currentRotation.y;
        }
        
        public void Update()
        {
            if (!Application.isFocused) return;
            
            _yRotation += Input.GetAxis("Mouse X") * _mouseSensitivity;
            _xRotation -= Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _xRotation = Mathf.Clamp(_xRotation, -MAX_VERTICAL_OFFSET, MAX_VERTICAL_OFFSET);
            
            Rotated?.Invoke(Quaternion.Euler(_xRotation, _yRotation, 0));
        }
    }
}