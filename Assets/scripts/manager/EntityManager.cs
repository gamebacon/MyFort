using System;
using DefaultNamespace;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace manager
{
    public class EntityManager : MonoBehaviour
    {

        [SerializeField] private GameObject npcPrefab;
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private GameObject rockPrefab;
    
        [FormerlySerializedAs("npcContainer")] [SerializeField] private Transform entityContainer;
    
        void Start()
        {
            Spawn(npcPrefab, 100);
            Spawn(treePrefab, 100, 1, 10f);
            Spawn(rockPrefab, 50, .4f, 2f);
        }

        private void Spawn(GameObject prefab, int amount, float minSIze = 1f, float maxSize = 1f)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject gameObject = Instantiate(prefab, Util.randomLocation(), Quaternion.Euler(0, Random.Range(0, 360), 0), entityContainer);
                gameObject.transform.localScale *= Random.Range(minSIze, maxSize);
            }
        }
    
    }
}
