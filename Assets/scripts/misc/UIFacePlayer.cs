using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UIFacePlayer : MonoBehaviour
{

    private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        double distanceSquared = Util.DistanceSquared(transform.position, player.transform.position);

        if (distanceSquared > 1_500)
        {
            return;
        }
        
        Vector3 direction = transform.position - player.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
}
