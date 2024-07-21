using System;
using System.Threading;
using System.Threading.Tasks;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Components
{
    [Serializable]
    public class MoveComponent
    {
        [SerializeField] private Transform _transform;

        public void Move(Vector3 position)
        {
            _transform.position = position;
        }
    }
}