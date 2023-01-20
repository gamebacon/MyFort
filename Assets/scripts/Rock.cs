using DefaultNamespace;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : Entity 
{
    private float size;

    private void Awake()
    {
        size = Random.Range(1.0f, 10f);
        onDeateEvent += Break;
    }

    private void Test()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        
        Vector3[] verts = mesh.vertices;
        
        print("Vertices: "+ verts.Length);
        
        for(int i = 0; i < verts.Length; i++)
        {
            Vector3 old = verts[i];
            
            verts[i] = new Vector3(
                old.x + Random.Range(1, 30f),
                old.y + Random.Range(1, 30f),
                old.z + Random.Range(1, 30f)
            );
            
        }
    }

    private void Break()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject stone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
            stone.name = "Stone" + i;
            stone.transform.localScale = Util.randomScale() * size * 0.25f;
            
            Item item = stone.AddComponent<Item>();
            item.SetType(ItemType.Stone);
            item.AddTag(Tag.Grabbable);
            
            stone.transform.position = transform.position;
        }
        
        Destroy(gameObject);
    }
}
