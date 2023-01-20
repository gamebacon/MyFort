using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color
            = Random.ColorHSV(0, 1f, 0, 1);

    }
}
