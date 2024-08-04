using System.Threading.Tasks;
using Core.Entity;
using Mirror;
using UnityEngine;

namespace Core
{
    public class CubeFactory
    {
        private const float RADIUS = 50f;
        private const float HEIGHT = 5f;
        private const float SPAWN_COOLDOWN = 2f;
        
        private readonly Cube _cubePrefab;
        private readonly Transform _worldTransform;

        private bool _isStarted;

        public CubeFactory(Cube cubePrefab, Transform worldTransform)
        {
            _cubePrefab = cubePrefab;
            _worldTransform = worldTransform;
        }

        public async void StartCubeSpawn()
        {
            _isStarted = true;
            
            while (_isStarted)
            {
                SpawnCube();
                await Task.Delay(Mathf.RoundToInt(SPAWN_COOLDOWN * 1000));
            }
        }
        
        public void StopCubeSpawn()
        {
            _isStarted = false;
        }
        
        private void SpawnCube()
        {
            var cube = Object.Instantiate(_cubePrefab, GetStartPosition(), Quaternion.identity, _worldTransform);
            
            NetworkServer.Spawn(cube.gameObject);
            //cube.RpcMove(GetStartPosition());
        }
        
        private Vector3 GetStartPosition()
        {
            var randomCirclePosition = Random.insideUnitCircle * RADIUS;
            
            return new Vector3(randomCirclePosition.x, Random.Range(0, HEIGHT), randomCirclePosition.y);
        }
    }
}