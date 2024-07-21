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

        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private float _mouseSensitivity;

        [SerializeField] private Renderer _renderer;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshCollider _meshCollider;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Material _material;

        [SerializeField] private float _length = 2.0f;
        [SerializeField] private float _radius = 2.0f;
        [SerializeField] private int _sections = 20;

        private PlayerScoreViewController _scoreViewController;
        
        private RotateToMouseMechanic _rotateToMouseMechanic;
        private CreateConeMechanic _createConeMechanic;

        private float _step;
        private float _cAngle;

        private readonly HashSet<Collider> _colliders = new();

        public override void OnStopLocalPlayer()
        {
            _scoreViewController.ResetScore();
            _scoreViewController.HideView();
        }
        
        private void Awake()
        {
            _scoreViewController = ServiceLocator.Resolve<PlayerScoreViewController>();

            _rotateToMouseMechanic = new RotateToMouseMechanic(_rotateComponent, _mouseSensitivity);
            _createConeMechanic = new CreateConeMechanic(_renderer, _meshFilter, _meshCollider, _bodyTransform,
                _material, _length, _radius, _sections);
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

                otherCollider.SendMessage(ON_TRIGGER_ENTER_FUNC_NAME, _meshCollider,
                    SendMessageOptions.DontRequireReceiver);
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