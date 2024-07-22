using System;
using System.Collections.Generic;
using Core.Components;
using Core.Mechanics;
using Core.UI.PlayerScore;
using Mirror;
using UnityEngine;

namespace Core.Entity
{
    public class Flashlight : NetworkBehaviour
    {
        private const string ON_TRIGGER_ENTER_FUNC_NAME = "OnTriggerExit";
        
        [Header("Mechanics")]
        [SerializeField] private RotateToMouseMechanic _rotateToMouseMechanic;
        [SerializeField] private CreateConeMechanic _createConeMechanic;

        [Header("Other")]
        [SerializeField] private MeshCollider _meshCollider;
        
        private readonly HashSet<Collider> _colliders = new();
        private PlayerScoreViewController _scoreViewController;

        public override void OnStopLocalPlayer()
        {
            _scoreViewController.ResetScore();
            _scoreViewController.HideView();
        }
        
        private void Awake()
        {
            _scoreViewController = ServiceLocator.Resolve<PlayerScoreViewController>();
        }

        private void Start()
        {
            _createConeMechanic.Initialize();

            if (!isLocalPlayer) return;

            _rotateToMouseMechanic.Initialize();
            _scoreViewController.ShowView();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            _rotateToMouseMechanic.Update();
        }

        private void OnDestroy()
        {
            if (!_meshCollider) return;

            foreach (var otherCollider in _colliders)
            {
                if (!otherCollider) continue;

                otherCollider.SendMessage(ON_TRIGGER_ENTER_FUNC_NAME, _meshCollider, SendMessageOptions.DontRequireReceiver);
            }

            _colliders.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            _colliders.Add(other);

            if (!isLocalPlayer) return;

            _scoreViewController.AddScore(1);
        }

        private void OnTriggerExit(Collider other)
        {
            _colliders.Remove(other);

            if (!isLocalPlayer) return;

            _scoreViewController.RemoveScore(1);
        }
    }
}