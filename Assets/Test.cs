using DefaultNamespace;
using UnityEngine;

public class Test : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    
    public float ThetaScale = 0.01f;
    public float radius = 3f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;


    
    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
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
    
    void Update() {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++) {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
    
}