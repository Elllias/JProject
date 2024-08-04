using System;
using System.Collections.Generic;
using Core.Entity;
using Mirror;
using UnityEngine;

namespace Core
{
    public class AdvancedNetworkManager : NetworkManager
    {
        [Header("Advanced Server Data")]
        [SerializeField] private Transform _worldTransform;

        private CubeFactory _cubeFactory;

        public override void Start()
        {
            base.Start();
            
            _cubeFactory = ServiceLocator.Resolve<CubeFactory>();
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        { 
            _cubeFactory.StartCubeSpawn();
        }
        
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            _cubeFactory.StopCubeSpawn();
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);

            conn.identity.AssignClientAuthority(conn);
            conn.identity.transform.SetParent(_worldTransform);
        }
    }
}