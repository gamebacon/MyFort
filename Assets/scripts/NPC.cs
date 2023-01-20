using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class NPC : Entity 
{
    [SerializeField] private NPCValues npcValues; 
    [SerializeField] private Animator _animator; 
    
    private Vector3 targetLocation;
    private Vector3 targetDirection;
    
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        npcValues.speedMultiplier *= Random.Range(1, 1.5f);
        _animator.speed = npcValues.speedMultiplier * 0.1f;
        onDeateEvent += onDeath;
        StartPath();
    }

    void Update()
    {

        if (isDead())
        {
            return;
        }
        
        double dist = Vector3.Distance(transform.position, targetLocation);
        npcValues.UI.distanceText.text = $"{dist:F0}m";

        if(dist <= 2f)
        {
            StartPath();
        }

        Vector3 force = targetDirection * npcValues.speedMultiplier; 
        _rb.AddForce(force);
    }


    private void onDeath()
    {
        onDeateEvent -= onDeateEvent;
        _animator.SetTrigger("death");
        Destroy(gameObject, 5);
    }

    private void StartPath()
    {
        int xRange = 100;
        int yRange = 100;

        targetLocation = Util.randomLocation();
        targetDirection = (targetLocation - transform.position).normalized;
        transform.LookAt(targetLocation);
    }


    private void OnCollisionEnter(Collision other)
    {
        TakeDamage(Random.Range(1f, 20f));
    }


    [Serializable]
    class NPCValues
    {
        public float speedMultiplier;
       [SerializeField] public NPCUI UI;
        
        [Serializable]
        internal class NPCUI {
            public TextMeshProUGUI distanceText;
        }
    }
    
}