using _GAME.Scripts.Base;
using _GAME.Scripts.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GAME.Scripts.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private Vector3[] _originalVertices;

        public void Init()
        {
            _meshFilter  = GetComponentInChildren<MeshFilter>();
            _mesh = _meshFilter.mesh;
            _originalVertices = _mesh.vertices.Clone() as Vector3[];
        }

        private void DeformMesh(Mesh mesh)
        {
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 randomDeformation = Random.insideUnitSphere * .2f;
                vertices[i] = Vector3.Lerp(vertices[i], _originalVertices[i] + randomDeformation, Time.deltaTime * 10);
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            _meshFilter.mesh = mesh;
        }
       

        public void SetupItemView()
        {
            DeformMesh(_mesh);
        }

    }
}
