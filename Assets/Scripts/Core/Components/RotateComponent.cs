using System;
using Mirror;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public class RotateComponent
    {
        [SerializeField] private Transform _transform;
        
        public void Rotate(Quaternion rotation)
        {
            _transform.localRotation = rotation;
        }
        
        public void RotateTo(Vector3 point)
        {
            _transform.LookAt(point);
        }

        public void RotateAround(Vector3 axis, float angle)
        {
            _transform.RotateAround(_transform.position, axis, angle);
        }

        public Vector3 GetCurrentRotation()
        {
            return _transform.localRotation.eulerAngles;
        }
    }
}