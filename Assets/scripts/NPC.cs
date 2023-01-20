using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class NPC : LivingEntity 
{

    private void OnCollisionEnter(Collision other)
    {
        TakeDamage(Random.Range(1f, 20f));
    }

    
}