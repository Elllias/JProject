using System;
using Core.Components;
using Core.Entity;
using UnityEngine;

namespace Core.Mechanics
{
    [Serializable]
    public class MoveThroughInputMechanic
    {
        public event Action<Vector3> Moved;
        
        [SerializeField] private Transform _orientation;
        [SerializeField] private float _moveSpeed = 0.2f;
        
        public void Update()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");

            var direction = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

            direction.y = 0;
            
            Moved?.Invoke(direction * _moveSpeed);
        }
    }
}