using DefaultNamespace;
using UnityEngine;

public class Test : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Geh();
    }

    void Geh()
    {
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Util.randomScale();
        }

        // assign the local vertices array into the vertices array of the Mesh.
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}