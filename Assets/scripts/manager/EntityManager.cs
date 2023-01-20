using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector3 = System.Numerics.Vector3;

public class EntityManager : MonoBehaviour
{

    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject rockPrefab;
    
    [FormerlySerializedAs("npcContainer")] [SerializeField] private Transform entityContainer;
    
    void Start()
    {
        Spawn(npcPrefab, 100);
        Spawn(treePrefab, 100, 5);
        Spawn(rockPrefab, 50, 1, 2f);
    }

    private void Spawn(GameObject prefab, int amount, float sizeFactor = 1, float sizeTresHold = .2f)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObject = Instantiate(prefab, Util.randomLocation(), Quaternion.Euler(0, Random.Range(0, 360), 0), entityContainer);
            gameObject.transform.localScale *= Random.Range(Math.Max(0.1f, 1 - sizeFactor), 1 + sizeTresHold) * sizeFactor;
        }
    }
    
}
