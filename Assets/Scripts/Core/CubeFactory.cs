using Core.Entity;
using Mirror;
using UnityEngine;

namespace Core
{
    public class CubeFactory
    {
        private readonly Cube _cubePrefab;
        private readonly Transform _worldTransform;

        public CubeFactory(Cube cubePrefab, Transform worldTransform)
        {
            _cubePrefab = cubePrefab;
            _worldTransform = worldTransform;
        }

        public void SpawnCube(Vector3 position, Vector3 ownerPosition)
        {
            var cube = Object.Instantiate(_cubePrefab, position, Quaternion.identity, _worldTransform);

            NetworkServer.Spawn(cube.gameObject);

            cube.Initialize();
            cube.RpcRotateToWithRandomAngle(ownerPosition);
        }
    }
}