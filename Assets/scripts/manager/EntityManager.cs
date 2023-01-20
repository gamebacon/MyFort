using DefaultNamespace;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class EntityManager : MonoBehaviour
{

    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject treePrefab;
    
    [SerializeField] private Transform npcContainer;
    
    void Start()
    {
            for (int i = 0; i < 100; i++)
            {
                Instantiate(npcPrefab, Util.randomLocation(), Quaternion.identity, npcContainer);
                GameObject tree = Instantiate(treePrefab, Util.randomLocation(), Quaternion.Euler(0, Random.Range(0, 360), 0), npcContainer);
                tree.transform.localScale *= Random.Range(1, 3);
            }

    }
}
