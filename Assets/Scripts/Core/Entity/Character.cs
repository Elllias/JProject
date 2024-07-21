using Core.Components;
using Core.Mechanics;
using Core.UI.PlayerScore;
using Mirror;
using UnityEngine;

namespace Core.Entity
{
    public class Character : NetworkBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private RotateComponent _cameraRotateComponent;

        [SerializeField] private float _mouseSensitivity;
        private RotateToMouseMechanic _rotateToMouseMechanic;
        
        private AdvancedNetworkManager _networkManager;
        private PlayerScoreViewController _scoreViewController;

        private void Awake()
        {
            _networkManager = ServiceLocator.Resolve<AdvancedNetworkManager>();

            _rotateToMouseMechanic = new RotateToMouseMechanic(_cameraRotateComponent, _mouseSensitivity);
            _rotateToMouseMechanic.Initialize();
        }
        
        private void Update()
        {
            _rotateToMouseMechanic.Update();
            
            if (!isLocalPlayer) return;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdSpawnCube();
            }
        }
        
        [ClientRpc]
        public void Move(Vector3 position)
        {
            _moveComponent.Move(position);
        }

        [ClientRpc]
        public void RotateTo(Vector3 point)
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