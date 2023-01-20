using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityValues entityValues = new EntityValues();

    protected delegate void OnDeath();
    protected OnDeath onDeateEvent;
    
    protected delegate void OnDamage(float damage);
    protected OnDamage onDamageEvent;


    private void Awake()
    {
        if (entityValues.ui.nameText)
        {
            entityValues.ui.nameText.text = gameObject.name;
        }
        
        entityValues.health = entityValues.maxHealth;
    }


    public bool isDead()
    {
        return entityValues.health <= 0;
    }


    public void TakeDamage(float damage)
    {
        onDamageEvent?.Invoke(damage);
        
        entityValues.health = Math.Max(0, entityValues.health - damage);

        float percentageLeft = entityValues.health / entityValues.maxHealth;

        EntityValues.UI ui = entityValues.ui;
        if (ui.healthBarSlider)
        {
            ui.healthBarSlider.value = percentageLeft;
        }
        

        
        if (entityValues.health == 0)
        {
            onDeateEvent?.Invoke();
        }
    }
    

    [Serializable]
    internal class EntityValues
    {
        [SerializeField] public float maxHealth;
        [SerializeField] public float health;
        [SerializeField] public UI ui = new UI();

        [Serializable]
        internal class UI 
        {
            public TextMeshProUGUI nameText;
            public Slider healthBarSlider;
        }
    }
}