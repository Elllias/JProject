using System;
using Core.Components;
using Core.Mechanics;
using Core.UI.PlayerScore;
using Mirror;
using UnityEngine;

namespace Core.Entity
{
    public class Character : NetworkBehaviour
    {
        [Header("Components")]
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        
        [Header("Mechanics")]
        [SerializeField] private RotateToMouseMechanic _rotateToMouseMechanic;
        
        private AdvancedNetworkManager _networkManager;
        private PlayerScoreViewController _scoreViewController;
        private InputHandler _inputHandler;

        private void Awake()
        {
            _networkManager = ServiceLocator.Resolve<AdvancedNetworkManager>();
            _inputHandler = ServiceLocator.Resolve<InputHandler>();

            _rotateToMouseMechanic.Initialize();
        }

        private void OnEnable()
        {
            _inputHandler.CubeSpawnButtonPressed += CmdSpawnCube;
        }

        private void OnDisable()
        {
            _inputHandler.CubeSpawnButtonPressed -= CmdSpawnCube;
        }

        private void Update()
        {
            _rotateToMouseMechanic.Update();
        }

        [ClientRpc]
        public void RpcMove(Vector3 position)
        {
            _moveComponent.Move(position);
        }

        [ClientRpc]
        public void RpcRotateTo(Vector3 point)
        {
            _rotateComponent.RotateTo(point);
        }

        [Command]
        private void CmdSpawnCube()
        {
            _networkManager.SpawnCube(Vector3.zero, transform.position);
        }
    }
}