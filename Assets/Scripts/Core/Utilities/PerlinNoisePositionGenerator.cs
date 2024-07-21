using UnityEngine;

namespace Core.Utilities
{
    public class PerlinNoisePositionGenerator
    {
        private readonly Vector3 _initialPosition;
        private const float AMPLITUDE = 1.5f;
        private const float PERIOD = 3.0f;
        
        private readonly float _perlinNoiseOffset;

        public PerlinNoisePositionGenerator(Vector3 initialPosition)
        {
            _initialPosition = initialPosition;
            _perlinNoiseOffset = Random.Range(0f, 100f);
        }

        public Vector3 GetPosition()
        {
            var newY = _initialPosition.y + Mathf.PerlinNoise(
                Time.time / PERIOD + _perlinNoiseOffset, 0) * AMPLITUDE - AMPLITUDE / 2;
            return new Vector3(_initialPosition.x, newY, _initialPosition.z);
        }
    }
}