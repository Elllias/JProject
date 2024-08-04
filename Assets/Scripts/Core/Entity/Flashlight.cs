using System;
using Core.Components;
using Core.Mechanics;
using Core.UI.PlayerScore;
using Mirror;
using UnityEngine;

namespace Core.Entity
{
    public class Flashlight : NetworkBehaviour
    {
        [Header("Mechanics")]
        [SerializeField] private CreateConeMechanic _createConeMechanic;
        [SerializeField] private ScoreControlMechanic _scoreControlMechanic;
        [SerializeField] private ColliderControlMechanic _colliderControlMechanic;

        public override void OnStopLocalPlayer()
        {
            _scoreControlMechanic.Stop();
        }

        private void Awake()
        {
            _scoreControlMechanic.Initialize();
        }

        private void Start()
        {
            _createConeMechanic.Start();

            if (!isLocalPlayer) return;
            
            _scoreControlMechanic.Start();
        }

        private void OnDestroy()
        {
            _colliderControlMechanic.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            _colliderControlMechanic.OnTriggerEnter(other);

            if (!isLocalPlayer) return;

            _scoreControlMechanic.OnTriggerEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            _colliderControlMechanic.OnTriggerExit(other);

            if (!isLocalPlayer) return;

            _scoreControlMechanic.OnTriggerExit();
        }
    }
}