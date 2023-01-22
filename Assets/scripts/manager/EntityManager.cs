using System;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

namespace manager
{
    public class EntityManager : MonoBehaviour
    {

        [SerializeField] private GameObject npcPrefab;
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private Transform entityContainer;
        [SerializeField] private LayerMask _resourceMask;
        
        
        void Start()
        {
            Spawn(npcPrefab, 1);
            Spawn(treePrefab, 10, 1, 10f);
            Spawn(rockPrefab, 10, .4f, 2f);
        }

        private void Spawn(GameObject prefab, int amount, float minSIze = 1f, float maxSize = 1f)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject gameObject = Instantiate(prefab, Util.randomLocation(), Quaternion.Euler(0, Random.Range(0, 360), 0), entityContainer);
                gameObject.transform.localScale *= Random.Range(minSIze, maxSize);
            }
        }

        public Type getResourceOrigin(ItemType type)
        {
            switch (type)
            {
                case ItemType.Wood:
                 return typeof(Tree);
                case ItemType.Stone:
                 return typeof(Rock);
                
                default: 
                    throw new Exception("No resource origin for " + type);
            }
        }

        public Resource FindResourceInRange(Transform origin, ItemType type, float range)
        {
            RaycastHit[] hits = Physics.SphereCastAll(origin.position, range, origin.forward,.1f, _resourceMask);
            
            
            foreach(RaycastHit hit in hits)
            {
                GameObject gameObject = hit.collider.gameObject;
                Resource resource = gameObject.GetComponentInParent<Resource>();
                if (resource.getDrop() == type)
                {
                    print("Found : " + gameObject.name + " - " + hit.distance + ", type: " + resource.GetType() + " - interest: " + type);
                    return resource;
                }
            }

            return null;
        }

        public enum EntityType
        {
            NPC,
            Tree,
            Rock
        }

    }
}
