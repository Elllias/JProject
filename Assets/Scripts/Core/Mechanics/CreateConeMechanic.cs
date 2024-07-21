using UnityEngine;

namespace Core.Mechanics
{
    public class CreateConeMechanic
    {
        private readonly Renderer _renderer;
        private readonly MeshFilter _meshFilter;
        private readonly MeshCollider _meshCollider;
        private readonly Transform _bodyTransform;
        private readonly Material _material;
        
        private readonly float _length;
        private readonly float _radius;
        private readonly int _sections;

        private float _step;
        private float _angle;

        public CreateConeMechanic(
            Renderer renderer,
            MeshFilter meshFilter,
            MeshCollider meshCollider,
            Transform bodyTransform,
            Material material,
            float length,
            float radius,
            int sections)
        {
            _renderer = renderer;
            _meshFilter = meshFilter;
            _meshCollider = meshCollider;
            _bodyTransform = bodyTransform;
            _material = material;
            _length = length;
            _radius = radius;
            _sections = sections;
        }

        public void Initialize()
        {
            if (_meshFilter == null)
            {
                Debug.LogError("MeshFilter not found!");
                return;
            }

            var mesh = _meshFilter.sharedMesh;
            
            if (mesh == null)
            {
                _meshFilter.mesh = new Mesh();
                mesh = _meshFilter.sharedMesh;
            }

            mesh.Clear();

            if (_sections < 3)
            {
                Debug.LogError("Number of viewcone sections must be 3 or more");
                return;
            }

            _step = 2 * Mathf.PI / _sections;
            _angle = 2 * Mathf.PI;
            
            var vertices = new Vector3[_sections + 1 + 1];
            
            vertices[0] = _bodyTransform.localPosition;
            
            for (var i = 1; i < _sections + 1; i++)
            {
                vertices[i] = new Vector3(Mathf.Sin(_angle) * _radius, Mathf.Cos(_angle) * _radius, 0);
                _angle += _step;
            }
            
            vertices[^1] = new Vector3(0, 0, _length);

            var index = 1;
            var indices = _sections * 3 * 2;
            var triangles = new int[indices];

            for (var i = 0; i < indices * 0.5; i += 3)
            {
                triangles[i] = 0;
                triangles[i + 1] = index;
                triangles[i + 2] = i >= indices * .5 - 3 ? 1 : index + 1;

                index++;
            }
            
            index = 1;
            
            for (var i = (int)(indices * 0.5); i < indices; i += 3)
            {
                triangles[i] = index;
                triangles[i + 1] = vertices.Length - 1;
                triangles[i + 2] = i >= indices - 3 ? 1 : index + 1;

                index++;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            
            _renderer.sharedMaterial = _material;

            if (_meshCollider != null)
            {
                _meshCollider.sharedMesh = mesh;
                _meshCollider.convex = true;
                _meshCollider.isTrigger = true;
            }

            _bodyTransform.localPosition = new Vector3(0, 0, _length);
        }
    }
}