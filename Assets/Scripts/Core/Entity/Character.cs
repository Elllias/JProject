using Core.Components;
using Core.Mechanics;
using Core.UI.PlayerScore;
using Mirror;
using UnityEngine;
using UnityEngine.Animations;

namespace Core.Entity
{
    public class Character : NetworkBehaviour
    {
        [Header("Mechanics")]
        [SerializeField] private RotateToMouseMechanic _rotateToMouseMechanic;
        [SerializeField] private MoveThroughInputMechanic _moveThroughInputMechanic;
        
        private PlayerScoreViewController _scoreViewController;

        private void Awake()
        {
            _rotateToMouseMechanic.Initialize(transform.localRotation);
        }

        private void OnEnable()
        {
            _moveThroughInputMechanic.Moved += MoveTo;
            _rotateToMouseMechanic.Rotated += Rotate;
        }

        private void OnDisable()
        {
            _moveThroughInputMechanic.Moved -= MoveTo;
            _rotateToMouseMechanic.Rotated -= Rotate;
        }
        
        private void Update()
        {
            if (!isLocalPlayer) return;
            
            _rotateToMouseMechanic.Update();
            _moveThroughInputMechanic.Update();
        }
        
        [Command]
        private void MoveTo(Vector3 direction)
        {
            transform.position += direction;
        }

        [Command]
        public void Rotate(Quaternion rotation)
        {
            transform.localRotation = rotation;
        }
    }
}