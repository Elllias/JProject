using System.Collections.Generic;
using Core.Entity;
using Mirror;
using UnityEngine;

namespace Core
{
    public class AdvancedNetworkManager : NetworkManager
    {
        private const float RADIUS = 35f;

        private readonly Vector3 _centerPoint = Vector3.zero;
        private readonly HashSet<Character> _players = new();

        private CubeFactory _cubeFactory;

        public override void Start()
        {
            base.Start();
            
            _cubeFactory = ServiceLocator.Resolve<CubeFactory>();
        }
        
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);

            conn.identity.AssignClientAuthority(conn);

            var player = conn.identity.gameObject.GetComponent<Character>();

            _players.Add(player);
            RepositionPlayers();
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            var player = conn.identity.gameObject.GetComponent<Character>();

            _players.Remove(player);
            RepositionPlayers();

            base.OnServerDisconnect(conn);
        }

        public void SpawnCube(Vector3 position, Vector3 ownerPosition)
        {
            _cubeFactory.SpawnCube(position, ownerPosition);
        }
        
        private void RepositionPlayers()
        {
            var playerCount = _players.Count;
            if (playerCount == 0) return;

            var angle = 360f / playerCount;
            var index = 0;

            foreach (var player in _players)
            {
                var angleInRad = index * angle * Mathf.Deg2Rad;
                var x = Mathf.Cos(angleInRad) * RADIUS;
                var z = Mathf.Sin(angleInRad) * RADIUS;

                player.Move(new Vector3(x, 0, z));
                player.RotateTo(_centerPoint);
                index += 1;
            }
        }
    }
}