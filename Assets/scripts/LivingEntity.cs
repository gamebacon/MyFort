using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LivingEntity : Entity
{
    public Action action = Action.Idle;
    [SerializeField] private LivingEntityValues livingEntityValues; 
    [SerializeField] private Animator _animator; 
    public Vector3 targetLocation;
    public Vector3 targetDirection;
    
    private Rigidbody _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _animator.speed = livingEntityValues.speedMultiplier * 0.1f;
        onDeateEvent += onDeath;

        DoSomethingNew();
    }

    private void Update()
    {
        switch (action)
        {
            case Action.Walk: 
            case Action.Run:
                Move();
                break;
            default: return;
        }
    }

    void Move()
    {
        double dist = Vector3.Distance(transform.position, targetLocation);

        if(dist <= 2f)
        {
            livingEntityValues.UI.distanceText.text = "";
            DoSomethingNew();
        }
        else
        {
            Vector3 force = targetDirection * livingEntityValues.speedMultiplier; 
            livingEntityValues.UI.distanceText.text = $"{dist:F0}m";
            _rb.AddForce(force);
        }
    }


    public enum Action
    {
        Idle,
        Walk,
        Run,
    }

    void DoSomethingNew()
    {
        Action newAction = decideNewAction();
        
        switch (newAction)
        {
            case Action.Walk:
            case Action.Run:
                targetLocation = Util.randomLocation();
                targetDirection = (targetLocation - transform.position).normalized;
                transform.LookAt(targetLocation);
                
                livingEntityValues.speedMultiplier = newAction == Action.Run ? 25 : 20;
                break;
            case Action.Idle:
                livingEntityValues.speedMultiplier = .3f;
                Idle(Random.Range(10f, 30f));
                break;
        }
        
        _animator.SetBool("run", newAction == Action.Run);
        _animator.SetBool("walk", newAction == Action.Walk);
        _animator.SetBool("idle", newAction == Action.Idle);
        
        _animator.SetFloat("speed", newAction == Action.Run ? 10f : newAction == Action.Walk ? 7f : 3f);
        
        livingEntityValues.UI.statusText.text = newAction.ToString();

        action = newAction;
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
            return Action.Run;
        } else if (num > 50)
        {
            return Action.Walk;
        }
        else
        {
            return Action.Idle;
        }
    }
    
    private void onDeath()
    {
        onDeateEvent -= onDeateEvent;
        _animator.SetTrigger("death");
        Destroy(gameObject, 5);
    } 
    
    void OnDrawGizmosSelected () {
        if (Application.isPlaying) {
            
            Gizmos.color = Color.white;
            
            if (action == Action.Run) {
                Gizmos.DrawLine(transform.position, targetLocation);
            }

            if (action == Action.Idle) {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position, 3.5f);
            }
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