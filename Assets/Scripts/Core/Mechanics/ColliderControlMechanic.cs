using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Mechanics
{
    [Serializable]
    public class ColliderControlMechanic
    {
        private const string ON_TRIGGER_ENTER_FUNC_NAME = "OnTriggerExit";
        
        [SerializeField] private Collider _collider;

        private HashSet<Collider> _colliders = new();

        public void OnTriggerEnter(Collider other)
        {
            _colliders.Add(other);
        }
        
        public void OnTriggerExit(Collider other)
        {
            _colliders.Remove(other);
        }

        public void Stop()
        {
            if (!_collider) return;

            foreach (var otherCollider in _colliders)
            {
                if (!otherCollider) continue;

                otherCollider.SendMessage(ON_TRIGGER_ENTER_FUNC_NAME, _collider, SendMessageOptions.DontRequireReceiver);
            }

            _colliders.Clear();
        }
    }
}