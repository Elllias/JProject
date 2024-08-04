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
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _highlightMaterial;

        private Material _defaultMaterial;
        private int _triggerCount;
        
        private PerlinNoisePositionGenerator _positionGenerator;
        
        private void Awake()
        {
            _defaultMaterial = _renderer.sharedMaterial;
            
            _positionGenerator = new PerlinNoisePositionGenerator(transform.position);
        }

        private void Start()
        {
            if (!isServer) return;
            
            RpcMove(transform.position);
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
        
        [ClientRpc]
        private void RpcMove(Vector3 position)
        {
            _moveComponent.Move(position);
        }
    }
}