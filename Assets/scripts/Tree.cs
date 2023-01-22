
using DefaultNamespace;
using UnityEngine;

public class Tree : Resource 
{

    [SerializeField] private GameObject[] branches;
    [SerializeField] private GameObject[] leaves;
        
    
    private void Start()
    {
        onDeateEvent += timber;
    }

    private void timber()
    {

        onDeateEvent -= timber;

        foreach (GameObject branch in branches)
        {
            branch.gameObject.AddComponent<Rigidbody>();
            Item item = branch.gameObject.AddComponent<Item>();
            

            if (item)
            {
                item.AddTag(Tag.Grabbable);
                item.SetType(ItemType.Wood);
            }
            
            
            branch.transform.SetParent(null);
        }
        foreach (GameObject leave in leaves)
        {
            Destroy(leave);
        }
    }
    
    
}
