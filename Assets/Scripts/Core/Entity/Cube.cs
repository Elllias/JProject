using System;
using Core.Components;
using Core.Mechanics;
using Core.Utilities;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Entity
{
    public class Cube : NetworkBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;

        [SerializeField] private float _radius;
        [SerializeField] private float _height;
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _highlightMaterial;

        private Material _defaultMaterial;
        private int _triggerCount;
        
        private PerlinNoisePositionGenerator _positionGenerator;

        private void Awake()
        {
            _defaultMaterial = _renderer.sharedMaterial;
        }
        
        public void Initialize()
        {
            var initialPosition = GetStartPosition();
            
            _positionGenerator = new PerlinNoisePositionGenerator(initialPosition);
            RpcMove(initialPosition);
        }

        private void Update()
        {
            if (!isServer) return;
            
            RpcMove(_positionGenerator.GetPosition());
        }

        private void OnTriggerEnter(Collider other)
        {
            _triggerCount += 1;
            _renderer.sharedMaterial = _highlightMaterial;
        }
        
        private void OnTriggerExit(Collider other)
        {
            _triggerCount -= 1;
            
            if (_triggerCount <= 0)
            {
                _renderer.sharedMaterial = _defaultMaterial;
            }
        }

        private Vector3 GetStartPosition()
        {
            var randomCirclePosition = Random.insideUnitCircle * _radius;
            
            return new Vector3(randomCirclePosition.x, Random.Range(0, _height), randomCirclePosition.y);
        }

        [ClientRpc]
        private void RpcMove(Vector3 position)
        {
            _moveComponent.Move(position);
        }
        
        [ClientRpc]
        public void RpcRotateToWithRandomAngle(Vector3 targetPosition)
        {
            var directionToPlayer = (targetPosition - transform.position).normalized;
            
            var lookRotation = Quaternion.FromToRotation(Vector3.one.normalized, directionToPlayer);
            
            var randomAngle = Random.Range(0f, 360f);
            var randomRotation = Quaternion.AngleAxis(randomAngle, Vector3.one.normalized);
            
            _rotateComponent.Rotate(lookRotation * randomRotation);
        }
    }
}