using System;
using DefaultNamespace;
using manager;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LivingEntity : Entity
{
    public Action action = Action.Idle;
    [SerializeField] private LivingEntityValues livingEntityValues; 
    [SerializeField] private Animator _animator; 
    public Vector3 targetLocation;
    
    private Rigidbody _rb;
    public float viewDistance { get; private set; }
    
    public ItemType resourceOfInterest;

    private EntityManager _entityManager;

    private Resource targetResource;

    private void OnEnable()
    {
        _entityManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EntityManager>();
        
        _rb = GetComponent<Rigidbody>();
        onDeateEvent += onDeath;
        DoSomethingNew();

        viewDistance = 15f;
    }

    private void Update()
    {
        switch (action)
        {
            case Action.Roam: 
                Move();
                break;
            default: return;
        }

        if (!targetResource && resourceOfInterest != ItemType.None)
        {
            SeekResource();
        }
        
    }

    void SeekResource()
    {
        Resource resource = _entityManager.FindResourceInRange(transform, resourceOfInterest, viewDistance);
        if (resource == null)
        {
            return;
        }
        
        targetResource = resource;
        targetLocation = resource.transform.position;
    }
    
    void Move()
    {
        double dist = Vector3.Distance(transform.position, targetLocation);

        if(dist <= .5f)
        {
            livingEntityValues.UI.distanceText.text = "";
            DoSomethingNew();
        }
        else
        {
            Vector3 targetDirection = (targetLocation - transform.position).normalized;
            float speed = 12 * livingEntityValues.speedMultiplier;
            Vector3 force = targetDirection * speed;
            livingEntityValues.UI.distanceText.text = $"{dist:F0}m";
            transform.LookAt(targetLocation);
            _rb.AddForce(force);
        }
    }


    public enum Action
    {
        Idle,
        Roam,
        Dead,
    }


    void FindResource()
    {
        resourceOfInterest = Util.getRandomItemType(true);

        if (resourceOfInterest == ItemType.None)
        {
            return;
        }

        Entity resource = _entityManager.FindResourceInRange(transform, resourceOfInterest, viewDistance);
    }
    
    
    void DoSomethingNew()
     {
        Action newAction = decideNewAction();
        
        FindResource();
        
        switch (newAction)
        {
            case Action.Roam:
                targetLocation = Util.randomLocation();
                break;
            case Action.Idle:
                Idle(Random.Range(10f, 30f));
                break;
        }
        
        // _animator.SetBool("run", newAction == Action.Run);
        _animator.SetBool("walk", newAction == Action.Roam);
        _animator.SetBool("idle", newAction == Action.Idle);
        
        // _animator.SetFloat("speed", newAction == Action.Run ? 1.6f : newAction == Action.Walk ? 1.2f : 1f);
        

        action = newAction;
        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        string status = action.ToString();

        if (resourceOfInterest != ItemType.None)
        {
            status = $"looking for {resourceOfInterest}";
        }
            
        livingEntityValues.UI.statusText.text = status;
    }

    void Idle(float time)
    {
        Invoke(nameof(DoSomethingNew), time);
    }
    

    private Action decideNewAction()
    {
        float num = Random.Range(1f, 100f);

        if (num > 90)
        {
            return Action.Idle;
        }
        else
        {
            return Action.Roam;
        }
    }
    
    private void onDeath()
    {
        action = Action.Dead;
        onDeateEvent -= onDeateEvent;
        _animator.SetTrigger("death");
        Destroy(gameObject, 5);
    } 
    
    void OnDrawGizmos() {
        if (Application.isPlaying) {

            if (action == Action.Roam)
            {
                Handles.Disc(
                    Quaternion.identity,
                    targetLocation,
                    Vector2.up,
                    1, false, 1f
                );
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewDistance);

            Handles.Disc(
                Quaternion.identity,
                transform.position,
                Vector2.up,
                viewDistance, false, 1f
            );
            
        }
    }

    [Serializable]
    class LivingEntityValues
    {
        public float speedMultiplier;
        [SerializeField] public NPCUI UI;
        
        [Serializable]
        internal class NPCUI {
            public TextMeshProUGUI distanceText;
            public TextMeshProUGUI statusText;
        }
    }
    
}